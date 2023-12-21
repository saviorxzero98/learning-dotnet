using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using System.Net.Http;
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

            const string customScheme = "JWT_OR_COOKIE";

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = customScheme;
                    options.DefaultChallengeScheme = customScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                              options =>
                {
                    options.Authority = Configuration["Authentication:OIDC:Authority"];
                    options.Audience = Configuration["Authentication:OIDC:ClientId"];
                    options.RequireHttpsMetadata = false;
                    //options.TokenValidationParameters = new TokenValidationParameters
                    //{
                    //    ValidateIssuer = false,
                    //    ValidateAudience = false,
                    //    ValidateLifetime = false,
                    //    ValidIssuer = Configuration["Authentication:OIDC:Authority"],
                    //    ValidAudience = Configuration["Authentication:OIDC:ClientId"],
                    //    ValidateIssuerSigningKey = false,
                    //    ClockSkew = TimeSpan.Zero
                    //};

                    // 自簽憑證的問題
                    options.BackchannelHttpHandler = new HttpClientHandler
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                        {
                            return true;
                        }
                    };
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                           options =>
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
                .AddPolicyScheme(customScheme, customScheme, options =>
                {
                    options.ForwardDefaultSelector = (context) =>
                    {
                        // filter by auth type
                        string authorization = context.Request.Headers[HeaderNames.Authorization];
                        if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                        {
                            return JwtBearerDefaults.AuthenticationScheme;
                        }

                        // otherwise always check for cookie auth
                        return CookieAuthenticationDefaults.AuthenticationScheme;
                    };
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.ClientId = Configuration["Authentication:OIDC:ClientId"];
                    options.ClientSecret = Configuration["Authentication:OIDC:ClientSecret"];
                    options.Authority = Configuration["Authentication:OIDC:Authority"];
                    options.MetadataAddress = Configuration["Authentication:OIDC:MetadataAddress"];
                    options.ResponseType = Configuration.GetValue("Authentication:OIDC:ResponseType", OpenIdConnectResponseType.Code);
                    options.ResponseMode = Configuration.GetValue("Authentication:OIDC:ResponseMode", OpenIdConnectResponseMode.Query);
                    options.RequireHttpsMetadata = false;
                    options.Scope.Clear();
                    Configuration.GetValue("Authentication:OIDC:Scope", "openid profile")
                                 .Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(s => options.Scope.Add(s));
                    options.SaveTokens = Configuration.GetValue("Authentication:OIDC:SaveTokens", false);


                    // 自簽憑證的問題
                    options.BackchannelHttpHandler = new HttpClientHandler
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                        {
                            return true;
                        }
                    };

                    options.Events.OnRemoteFailure = context =>
                    {
                        var failure = context.Failure;
                        if (!string.IsNullOrWhiteSpace(failure?.Message))
                        {
                            //logger.Error(failure, "{Exception}", failure.Message);
                        }

                        context.Response.Redirect("./Account/ExternalLoginFailure");
                        context.HandleResponse();
                        return Task.CompletedTask;
                    };
                });




            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //    {
            //        // Logout
            //        options.Events.OnSigningOut = context =>
            //        {
            //            var redirectContext = new RedirectContext<CookieAuthenticationOptions>(
            //                context.HttpContext,
            //                context.Scheme,
            //                context.Options,
            //                context.Properties,
            //                "/"
            //            );
            //            if (Configuration.GetValue("Authentication:CAS:SingleSignOut", false))
            //            {
            //                // Single Sign-Out
            //                var casUrl = new Uri(Configuration["Authentication:CAS:ServerUrlBase"]);
            //                var request = context.Request;
            //                var serviceUrl = context.Properties.RedirectUri ??
            //                                $"{request.Scheme}://{request.Host}/{request.PathBase}";
            //                redirectContext.RedirectUri = UriHelper.BuildAbsolute(
            //                    casUrl.Scheme,
            //                    new HostString(casUrl.Host, casUrl.Port),
            //                    casUrl.LocalPath, "/logout",
            //                    QueryString.Create("service", serviceUrl));
            //            }

            //            context.Options.Events.RedirectToLogout(redirectContext);
            //            return Task.CompletedTask;
            //        };
            //    })
            //    .AddOpenIdConnect(options =>
            //    {
            //        options.ClientId = Configuration["Authentication:OIDC:ClientId"];
            //        options.ClientSecret = Configuration["Authentication:OIDC:ClientSecret"];
            //        options.Authority = Configuration["Authentication:OIDC:Authority"];
            //        options.MetadataAddress = Configuration["Authentication:OIDC:MetadataAddress"];
            //        options.ResponseType = Configuration.GetValue("Authentication:OIDC:ResponseType", OpenIdConnectResponseType.Code);
            //        options.ResponseMode = Configuration.GetValue("Authentication:OIDC:ResponseMode", OpenIdConnectResponseMode.Query);
            //        options.RequireHttpsMetadata = false;
            //        options.Scope.Clear();
            //        Configuration.GetValue("Authentication:OIDC:Scope", "openid profile")
            //                     .Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(s => options.Scope.Add(s));
            //        options.SaveTokens = Configuration.GetValue("Authentication:OIDC:SaveTokens", false);


            //        // 自簽憑證的問題
            //        options.BackchannelHttpHandler = new HttpClientHandler
            //        {
            //            ClientCertificateOptions = ClientCertificateOption.Manual,
            //            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
            //            {
            //                return true;
            //            }
            //        };

            //        options.Events.OnRemoteFailure = context =>
            //        {
            //            var failure = context.Failure;
            //            if (!string.IsNullOrWhiteSpace(failure?.Message))
            //            {
            //                //logger.Error(failure, "{Exception}", failure.Message);
            //            }

            //            context.Response.Redirect("./Account/ExternalLoginFailure");
            //            context.HandleResponse();
            //            return Task.CompletedTask;
            //        };
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
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
