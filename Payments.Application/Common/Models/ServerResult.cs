using System.Collections.Generic;

namespace Payments.Application.Common.Models
{
    public class ServerResult<T> where T: class
    {
        public ServerResult(bool succeeded, Dictionary<string, string> errors, T data)
        {
            Succeeded = succeeded;
            Errors = errors;
            Data = data;
        }

        public bool Succeeded { get; set; }

        public Dictionary<string, string> Errors { get; set; }

        public T Data { get; set; }

        public static ServerResult<T> Success(T data)
        {
            return new ServerResult<T>(true, null, data);
        }

        public static ServerResult<T> Failure(Dictionary<string, string> errors)
        {
            return new ServerResult<T>(false, errors, default);
        }
    }
}
