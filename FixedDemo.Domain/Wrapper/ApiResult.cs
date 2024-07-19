using System.Net;

namespace FixedDemo.Domain.Wrapper
{
    public class ApiResult<TData>
    {
        public ApiResult()
        {
            
        }

        public TData? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = [];
        public HttpStatusCode StatusCode { get; set; }
        public static ApiResult<TData> Success(TData data, HttpStatusCode statusCode = HttpStatusCode.OK, string message = "")
        {
            return new ApiResult<TData> { Data = data, IsSuccess = true, Message = message, StatusCode = statusCode };
        }
        public static ApiResult<TData> Success(string message = "", HttpStatusCode statusCode = HttpStatusCode.NoContent)
        {
            return new ApiResult<TData> { Message = message, StatusCode = statusCode, IsSuccess = true };
        }
        public static ApiResult<TData> Fail(List<string>? errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string message = "")
        {
            return new ApiResult<TData> { IsSuccess = false, Message = message, Errors = errors, StatusCode = statusCode };
        }
        public static ApiResult<TData> Fail(string? error = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string message = "")
        {
            return new ApiResult<TData> { IsSuccess = false, Message = message, Errors = [error], StatusCode = statusCode };
        }
        public static ApiResult<TData> ValidationError(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResult<TData> { Data = default, StatusCode = statusCode, Errors = errors, IsSuccess = false, Message = "Validation" };
        }
        public static ApiResult<TData> ValidationError(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResult<TData> { Data = default, StatusCode = statusCode, Errors = [error], IsSuccess = false, Message = "Validation" };
        }

    }
}
