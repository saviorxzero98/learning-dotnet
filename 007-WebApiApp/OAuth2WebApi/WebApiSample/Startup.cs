using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddAuthorization(options =>
            {
                // Globally Require Authenticated Users
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters();
                  })
                  .AddCookie(options =>
                  {
                      // Logout
                      options.Events.OnSigningOut = context =>
                      {
                          var redirectContext = new RedirectContext<CookieAuthenticationOptions>(
                              context.HttpContext,
                              context.Scheme,
                              context.Options,
                              context.Properties,
                              "/"
                          );
                          if (Configuration.GetValue("Authentication:CAS:SingleSignOut", false))
                          {
                              // Single Sign-Out
                              var casUrl = new Uri(Configuration["Authentication:CAS:ServerUrlBase"]);
                              var request = context.Request;
                              var serviceUrl = context.Properties.RedirectUri ??
                                               $"{request.Scheme}://{request.Host}/{request.PathBase}";
                              redirectContext.RedirectUri = UriHelper.BuildAbsolute(
                                  casUrl.Scheme,
                                  new HostString(casUrl.Host, casUrl.Port),
                                  casUrl.LocalPath, "/logout",
                                  QueryString.Create("service", serviceUrl));
                          }

                          context.Options.Events.RedirectToLogout(redirectContext);
                          return Task.CompletedTask;
                      };
                  })
                  .AddOAuth(OAuthDefaults.DisplayName, options =>
                  {
                      options.CallbackPath = "/signin";
                      options.ClientId = Configuration["Authentication:OAuth:ClientId"];
                      options.ClientSecret = Configuration["Authentication:OAuth:ClientSecret"];
                      options.AuthorizationEndpoint = Configuration["Authentication:OAuth:AuthorizationEndpoint"];
                      options.TokenEndpoint = Configuration["Authentication:OAuth:TokenEndpoint"];
                      options.UserInformationEndpoint = Configuration["Authentication:OAuth:UserInformationEndpoint"];
                      Configuration.GetValue("Authentication:OAuth:Scope", "email")
                          .Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(s => options.Scope.Add(s));
                      options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                      options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                      options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                      options.ClaimActions.MapJsonSubKey(ClaimTypes.Name, "attributes", "display_name");
                      options.ClaimActions.MapJsonSubKey(ClaimTypes.Email, "attributes", "email");
                      options.SaveTokens = Configuration.GetValue("Authentication:OAuth:SaveTokens", false);

                      // 自簽憑證的問題
                      options.BackchannelHttpHandler = new HttpClientHandler
                      {
                          ClientCertificateOptions = ClientCertificateOption.Manual,
                          ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                          {
                              return true;
                          }
                      };

                      options.Events.OnCreatingTicket = async context =>
                      {
                          // Get the OAuth user
                          using var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                          request.Headers.Accept.ParseAdd("application/json");
                          if (Configuration.GetValue("Authentication:OAuth:SendAccessTokenInQuery", false))
                          {
                              request.RequestUri = new Uri(QueryHelpers.AddQueryString(request.RequestUri!.OriginalString, "access_token", context.AccessToken!));
                          }
                          else
                          {
                              request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                          }

                          using var response = await context.Backchannel
                                                            .SendAsync(request, context.HttpContext.RequestAborted)
                                                            .ConfigureAwait(false);

                          if (!response.IsSuccessStatusCode || response.Content.Headers.ContentType?.MediaType?.StartsWith("application/json") != true)
                          {
                              throw new HttpRequestException($"An error occurred when retrieving OAuth user information ({response.StatusCode}). Please check if the authentication information is correct.");
                          }

                          await using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                          using var json = await JsonDocument.ParseAsync(stream).ConfigureAwait(false);
                          context.RunClaimActions(json.RootElement);
                      };
                  });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapFallbackToFile("index.html").AllowAnonymous()
                //            // only match 'GET' and 'HEAD' requests, fixed since .NET 7
                //            // https://github.com/aspnet/Announcements/issues/495
                //         .WithMetadata(new HttpMethodMetadata(new[] { HttpMethod.Get.Method, HttpMethod.Head.Method }));
            });
        }
    }
}
