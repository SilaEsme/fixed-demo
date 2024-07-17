using System.Net;

namespace FixedDemo.Domain.Wrapper
{
    public class ApiResult<TData>
    {
        public TData? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = [];
        public HttpStatusCode StatusCode { get; set; }
        public static ApiResult<TData> Success(TData data, string message = "", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResult<TData> { Data = data, IsSuccess = true, Message = message, StatusCode = statusCode };
        }
        public static ApiResult<TData> Success(string message = "", HttpStatusCode statusCode = HttpStatusCode.NoContent)
        {
            return new ApiResult<TData> { IsSuccess = true, Message = message, StatusCode = statusCode };
        }
        public static ApiResult<TData> Fail(string message = "", List<string>? errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return new ApiResult<TData> { IsSuccess = false, Message = message, Errors = errors, StatusCode = statusCode };
        }
    }
}
