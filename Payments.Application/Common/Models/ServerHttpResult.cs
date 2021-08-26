using System.Collections.Generic;
using System.Net;

namespace Payments.Application.Common.Models
{
    public class ServerHttpResult<T> : ServerResult<T> where T : class
    {
        public ServerHttpResult(
            bool succeeded, Dictionary<string, string> errors, HttpStatusCode httpCode, string message, T data)
            : base(succeeded, errors, data)
        {
            HttpCode = httpCode;
            Message = message;
        }

        public HttpStatusCode HttpCode { get; set; }
        public string Message { get; set; }


        public static ServerHttpResult<T> HttpSuccess(T data)
        {
            return new ServerHttpResult<T>(true, null, HttpStatusCode.OK, null, data);
        }

        public static ServerHttpResult<T> HttpFailure(Dictionary<string, string> errors, HttpStatusCode httpCode, string message)
        {
            return new ServerHttpResult<T>(false, errors, httpCode, message, default);
        }
    }
}
