using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebApiSample.Models;
using WebApiSample.Toolkits;

namespace WebApiSample.Controllers
{
    /// <summary>
    /// Demo
    /// </summary>
    [Route("api")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DemoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="id"></param>
        /// <param name="action"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Version
        /// </summary>
        /// <returns></returns>
        // GET api/version
        [HttpGet]
        [Route("version")]
        public ActionResult GetVersion()
        {
            var version = _configuration.GetSection("Version").Value.ToString();

            return Ok(version);
        }

        /// <summary>
        /// 取得書籍
        /// </summary>
        /// <param name="fromUser"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("book")]
        public ActionResult GetBook([FromQuery(Name = "fromUser")] string fromUser)
        {
            (bool result, string value) = HttpRequestHelper.GetHeader(Request, "Authorization");

            if (result && !string.IsNullOrEmpty(value))
            {
                return new JsonResult(new Book()
                {
                    Id = Guid.NewGuid(),
                    Name = "Alice's Adventures in Wonderland",
                    Episode = 1,
                    Price = 500,
                    Author = new Author()
                    {
                        Uid = 1,
                        Name = "Charles Lutwidge Dodgson"
                    },
                    PublishDate = new DateTime(1865, 7, 4),
                    IsReprint = false
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// 取得多個書籍
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("books")]
        public ActionResult GetBooks()
        {
            (bool result, string value) = HttpRequestHelper.GetHeader(Request, "Authorization");

            if (result && !string.IsNullOrEmpty(value))
            {
                var users = new List<Book>()
                {
                    new Book()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Alice's Adventures in Wonderland",
                        Episode = 1,
                        Price = 500,
                        Author = new Author()
                        {
                            Uid = 1,
                            Name = "Charles Lutwidge Dodgson"
                        },
                        PublishDate = new DateTime(1865, 7, 4),
                        IsReprint = false
                    },
                    new Book()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cendrillon",
                        Episode = 6,
                        Price = 500,
                        Author = new Author()
                        {
                            Uid = 2,
                            Name = "Charles Perrault"
                        },
                        PublishDate = new DateTime(1697, 1, 4),
                        IsReprint = false
                    },
                    new Book()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Romeo and Juliet",
                        Episode = 1,
                        Price = 500,
                        Author = new Author()
                        {
                            Uid = 1,
                            Name = "William Shakespeare"
                        },
                        PublishDate = new DateTime(1595, 1, 1),
                        IsReprint = false
                    },
                    new Book()
                    {
                        Id = Guid.NewGuid(),
                        Name = "浦島伝説",
                        Episode = 1,
                        Price = 500,
                        Author = new Author(),
                        PublishDate = new DateTime(713, 1, 1),
                        IsReprint = false
                    }
                };

                return new JsonResult(users);
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// 新增書籍 (JSON Raw)
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("book/json")]
        public ActionResult PostJson([FromBody] Book book)
        {
            return new JsonResult(new
            {
                Id = book.Id,
                Name = book.Name,
                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
        }

        /// <summary>
        /// 新增書籍 (Form Data)
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("book/form")]
        public ActionResult PostForm([FromForm] Book book)
        {
            return new JsonResult(new
            {
                Id = book.Id,
                Name = book.Name,
                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
        }
    }
}
