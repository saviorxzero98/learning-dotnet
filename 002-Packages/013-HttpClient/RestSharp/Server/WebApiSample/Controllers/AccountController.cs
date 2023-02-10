using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // GET api/account/json
        [HttpGet]
        [Route("account/json")]
        public IActionResult GetAccountAsJson()
        {
            (bool resut, string value) = GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                return new JsonResult(new AccountProfile()
                {
                    Id = 1,
                    Name = "Ace",
                    IsBlocked = false,
                    CreateDate = DateTime.Today
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET api/account/xml
        [HttpGet]
        [Route("account/xml")]
        [Produces("application/xml")]
        public IActionResult GetAccountAsXml()
        {
            (bool resut, string value) = GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                return new ObjectResult(new AccountProfile()
                {
                    Id = 1,
                    Name = "Ace",
                    IsBlocked = false,
                    CreateDate = DateTime.Today
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST api/account/json/no_content
        [HttpPost]
        [Route("account/json/no_content")]
        public IActionResult PostAccountAsJsonWithNoneContent([FromBody] AccountProfile account)
        {
            (bool resut, string value) = GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST api/account/json/text
        [HttpPost]
        [Route("account/text")]
        public IActionResult PostAccountAsText([FromBody] string account)
        {
            (bool resut, string value) = GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                var token = Hash($"{account}|{DateTime.Now.Ticks}");

                return new JsonResult(new
                {
                    Id = account,
                    Name = account,
                    Token = token,
                    Expires = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST api/account/json
        [HttpPost]
        [Route("account/json")]
        public IActionResult PostAccountAsJson([FromBody] AccountProfile account)
        {
            (bool resut, string value) = GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                var token = Hash($"{account.Name}|{DateTime.Now.Ticks}");

                return new JsonResult(new
                {
                    Id = account.Id,
                    Name = account.Name,
                    Token = token,
                    Expires = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST api/account/xml
        [HttpPost]
        [Route("account/xml")]
        [Consumes("application/xml")]
        public IActionResult PostAccountAsXml([FromBody] AccountProfile account)
        {
            (bool resut, string value) = GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                var token = Hash($"{account.Name}|{DateTime.Now.Ticks}");

                return new ObjectResult(new
                {
                    Id = account.Id,
                    Name = account.Name,
                    Token = token,
                    Expires = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST api/account/form/no_content
        [HttpPost]
        [Route("account/form/no_content")]
        public IActionResult PostAccountAsFormWithNoContent([FromForm] AccountProfile account)
        {
            (bool resut, string value) = GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }


        // POST api/account/form
        [HttpPost]
        [Route("account/form")]
        public IActionResult PostAccountAsForm([FromForm] AccountProfile account)
        {
            (bool resut, string value) = GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                var token = Hash($"{account.Name}|{DateTime.Now.Ticks}");
                return new JsonResult(new
                {
                    Id = account.Id,
                    Name = account.Name,
                    Token = token,
                    Expires = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            else
            {
                return Unauthorized();
            }
        }


        /// <summary>
        /// Hash
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Header
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected (bool result, string value) GetHeader(HttpRequest request, string key)
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
