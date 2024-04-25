using Microsoft.AspNetCore.Mvc;
using System;
using WebApiSample.Models;
using WebApiSample.Toolkits;

namespace WebApiSample.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 取得帳號資訊 (JSON)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("account/json")]
        public IActionResult GetAccountAsJson()
        {
            (bool resut, string value) = HttpRequestHelper.GetHeader(Request, "Authorization");

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

        /// <summary>
        /// 取得帳號資訊 (XML)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("account/xml")]
        [Produces("application/xml")]
        public IActionResult GetAccountAsXml()
        {
            (bool resut, string value) = HttpRequestHelper.GetHeader(Request, "Authorization");

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

        /// <summary>
        /// 新增帳號資訊
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("account/json/no_content")]
        public IActionResult PostAccountAsJsonWithNoneContent([FromBody] AccountProfile account)
        {
            (bool resut, string value) = HttpRequestHelper.GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// 新增帳號資訊 (Text)
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("account/text")]
        public IActionResult PostAccountAsText([FromBody] string account)
        {
            (bool resut, string value) = HttpRequestHelper.GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                var token = HashHelper.Hash($"{account}|{DateTime.Now.Ticks}");

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

        /// <summary>
        /// 新增帳號資訊 (JSON)
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("account/json")]
        public IActionResult PostAccountAsJson([FromBody] AccountProfile account)
        {
            (bool resut, string value) = HttpRequestHelper.GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                var token = HashHelper.Hash($"{account.Name}|{DateTime.Now.Ticks}");

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
        /// 新增帳號資訊 (XML)
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("account/xml")]
        [Consumes("application/xml")]
        public IActionResult PostAccountAsXml([FromBody] AccountProfile account)
        {
            (bool resut, string value) = HttpRequestHelper.GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                var token = HashHelper.Hash($"{account.Name}|{DateTime.Now.Ticks}");

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

        /// <summary>
        /// 新增帳號資訊 (Form Data)
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("account/form/no_content")]
        public IActionResult PostAccountAsFormWithNoContent([FromForm] AccountProfile account)
        {
            (bool resut, string value) = HttpRequestHelper.GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }


        /// <summary>
        /// 新增帳號資訊 (Form Data)
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("account/form")]
        public IActionResult PostAccountAsForm([FromForm] AccountProfile account)
        {
            (bool resut, string value) = HttpRequestHelper.GetHeader(Request, "Authorization");

            if (resut && !string.IsNullOrEmpty(value))
            {
                var token = HashHelper.Hash($"{account.Name}|{DateTime.Now.Ticks}");
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
    }
}
