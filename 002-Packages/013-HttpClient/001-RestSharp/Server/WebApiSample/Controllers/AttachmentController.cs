using HeyRed.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiSample.Models;
using WebApiSample.Toolkits;

namespace WebApiSample.Controllers
{
    [Route("api")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private const string AttachmentFolder = "~\\Public";
        private string AttachmentPath
        {
            get
            {
                return PathHelper.ToAbsolutePath(AttachmentFolder, AppDomain.CurrentDomain.BaseDirectory);
            }
        }

        [HttpPost]
        [Route("files/upload")]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            var size = files.Sum(f => f.Length);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string filePath = PathHelper.CombinePath(AttachmentPath, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }


            return Ok(new
            {
                Count = files.Count,
                Size = size
            });
        }

        [HttpPost]
        [Route("attachment/upload")]
        public async Task<IActionResult> UploadAttachment([FromForm] AttachmentInfo attachment)
        {
            var files = attachment.Files;
            var size = files.Sum(f => f.Length);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string filePath = PathHelper.CombinePath(AttachmentPath, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new
            {
                Count = files.Count,
                Size = size
            });
        }


        [HttpGet]
        [Route("attachment/download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return NotFound();
            }

            string filePath = PathHelper.CombinePath(AttachmentPath, fileName);
            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Seek(0, SeekOrigin.Begin);

            string contentType = MimeTypesMap.GetMimeType(fileName);
            return new FileStreamResult(memoryStream, contentType);
        }
    }
}
