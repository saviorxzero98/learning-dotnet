using Microsoft.AspNetCore.Mvc;

namespace RefitWebApiServer.Models
{
    public class HttpResponseResult
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public HttpResponseResult()
        {

        }
        public HttpResponseResult(int status, string message = "", object data = null)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// 建立 Response
        /// </summary>
        /// <returns></returns>
        public ObjectResult Create()
        {
            return new ObjectResult(Data)
            {
                StatusCode = Status
            };
        }

        /// <summary>
        /// 建立 Response
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ObjectResult Send(int status, string message, object data = null)
        {
            HttpResponseResult result = new HttpResponseResult(status, message, data);
            return result.Create();
        }


        #region Common

        /// <summary>
        /// Send 200 OK
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ObjectResult Ok(string message = "OK", object data = null)
        {
            return Send(StatusCodes.Status200OK, message, data);
        }

        /// <summary>
        /// Send 202 Accepted
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ObjectResult Accepted(string message = "Accepted", object data = null)
        {
            return Send(StatusCodes.Status202Accepted, message, data);
        }

        /// <summary>
        /// Send 204 NoContent
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ObjectResult NoContent(string message = "No Content", object data = null)
        {
            return Send(StatusCodes.Status204NoContent, message, data);
        }

        /// <summary>
        /// Send 400 Bad Request
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ObjectResult BadRequest(string message = "Bad Request", object data = null)
        {
            return Send(StatusCodes.Status400BadRequest, message, data);
        }

        /// <summary>
        /// Send 401 Unauthorized
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ObjectResult Unauthorized(string message = "Unauthorized", object data = null)
        {
            return Send(StatusCodes.Status401Unauthorized, message, data);
        }

        /// <summary>
        /// Send 403 Forbidden
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ObjectResult Forbidden(string message = "Forbidden", object data = null)
        {
            return Send(StatusCodes.Status403Forbidden, message, data);
        }

        /// <summary>
        /// Send 404 Not Found
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ObjectResult NotFound(string message = "Not Found", object data = null)
        {
            return Send(StatusCodes.Status404NotFound, message, data);
        }

        /// <summary>
        /// Send 500 Internal Server Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ObjectResult InternalServerError(string message = "Internal Server Error", object data = null)
        {
            return Send(StatusCodes.Status500InternalServerError, message, data);
        }

        /// <summary>
        /// Send 501 Not Implemented
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ObjectResult NotImplemented(string message = "Not Implemented", object data = null)
        {
            return Send(StatusCodes.Status501NotImplemented, message, data);
        }

        #endregion
    }
}
