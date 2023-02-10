using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    [Route("api")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DemoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("test/{id}")]
        public ActionResult GetId(string id, [FromQuery] string action)
        {
            if (string.IsNullOrEmpty(action))
            {
                return Ok(id);
            }
            else
            {
                return StatusCode(500);
                //return Ok(new { id = id, actopm = action });
            }
        }

        // GET api/version
        [HttpGet]
        [Route("version")]
        public ActionResult GetVersion()
        {
            var version = _configuration.GetSection("Version").Value.ToString();

            return Ok(version);
        }

        // GET api/country
        [HttpGet]
        [Route("country")]
        public ActionResult GetCountry([FromQuery(Name = "fromUser")] string fromUser)
        {
            (bool resut, string value) = GetHeader(Request, "Authorization");
            
            if (resut && !string.IsNullOrEmpty(value))
            {
                return new JsonResult(new CountryProfile()
                {
                    Id = "ROC",
                    Name = "中華民國",
                    Password = fromUser,
                    Birth = DateTime.Parse("1912-10-10").ToString("yyyy-MM-dd")
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET api/countries
        [HttpGet]
        [Route("countries")]
        public ActionResult GetCountrys()
        {
            (bool resut, string value) = GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                var users = new List<CountryProfile>()
            {
                new CountryProfile()
                {
                    Id = "ROC",
                    Name = "中華民國",
                    Password = "********",
                    Birth = DateTime.Parse("1912-10-10").ToString("yyyy-MM-dd")
                },
                new CountryProfile()
                {
                    Id = "USA",
                    Name = "美利堅合眾國",
                    Password = "********",
                    Birth = DateTime.Parse("1776-07-04").ToString("yyyy-MM-dd")
                },
                new CountryProfile()
                {
                    Id = "KOR",
                    Name = "大韓民國",
                    Password = "********",
                    Birth = DateTime.Parse("1945-08-15").ToString("yyyy-MM-dd")
                },
                new CountryProfile()
                {
                    Id = "POC",
                    Name = "中華人民共和國",
                    Password = "********",
                    Birth = DateTime.Parse("1949-10-01").ToString("yyyy-MM-dd")
                }
            };

                return new JsonResult(users);
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST api/country/json
        [HttpPost]
        [Route("country/json")]
        public ActionResult PostJson([FromBody] CountryProfile country)
        {
            (bool resut, string value) = GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                var token = Hash($"{country.Id}|{country.Password}|{DateTime.Now.Ticks}");
                return new JsonResult(new
                {
                    Id = country.Id,
                    Name = country.Name,
                    Token = token,
                    Expires = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST api/country/form
        [HttpPost]
        [Route("country/form")]
        public ActionResult PostForm([FromForm] CountryProfile country)
        {
            (bool resut, string value) = GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                var token = Hash($"{country.Id}|{country.Password}|{DateTime.Now.Ticks}");
                return new JsonResult(new
                {
                    Id = country.Id,
                    Name = country.Name,
                    Token = token,
                    Expires = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        protected string Hash(string plainText)
        {
            // Hash
            HashAlgorithm algorithm = new SHA256CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            byte[] crypto = algorithm.ComputeHash(bytes);

            // To Base64
            var result = Convert.ToBase64String(crypto);
            return result.Replace("=", "")
                         .Replace("/", "_")
                         .Replace("+", "-");
        }

        public (bool result, string value) GetHeader(HttpRequest request, string key)
        {
            if (request.Headers.ContainsKey(key))
            {
                var value = request.Headers[key].FirstOrDefault();
                return (result: true, value: value);
            }
            else
            {
                return (result: true, value: string.Empty);
            }
        }
    }
}
