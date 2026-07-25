namespace Safqat.Api.Models;

public class ApiResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; } = new();
    public int StatusCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string TraceId { get; set; }
    public static ApiResult<T> Ok(T data, string message = "Success")
        => new()
        {
            Success = true,
            Data = data,
            Message = message,
            StatusCode = 200
        };
    public static ApiResult<T> Fail(string message, List<string> errors = null)
    => new()
    {
        Success = false,
        Message = message,
        StatusCode = 400,
        Errors = errors ?? new()
    };
    public static ApiResult<T> Created(T data, string message = "Resource created")
        => new()
        {
            Success = true,
            Data = data,
            Message = message,
            StatusCode = 201
        };
    public static ApiResult<T> BadRequest(string message, List<string> errors = null)
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = 400,
            Errors = errors ?? new()
        };
    public static ApiResult<T> Unauthorized(string message = "Unauthorized")
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = 401
        };
    public static ApiResult<T> Forbidden(string message = "Forbidden")
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = 403
        };
    public static ApiResult<T> NotFound(string message = "Resource not found")
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = 404
        };
    public static ApiResult<T> Conflict(string message = "Conflict occurred")
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = 409
        };
    public static ApiResult<T> InternalServerError(string message = "An error occurred")
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = 500
        };
}

public class ApiResult : ApiResult<object>
{
    public static ApiResult Ok(string message = "Success")
        => new()
        {
            Success = true,
            Message = message,
            StatusCode = 200
        };
    public new static ApiResult Fail(string message, List<string> errors = null)
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = 400,
            Errors = errors ?? new()
        };
    public new static ApiResult BadRequest(string message, IEnumerable<string> errors = null)
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = 400,
            Errors = errors?.ToList() ?? new()
        };
    public new static ApiResult Unauthorized(string message = "Unauthorized")
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = 401
        };
    public new static ApiResult Forbidden(string message = "Forbidden")
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = 403
        };
    public new static ApiResult NotFound(string message = "Resource not found")
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = 404
        };
    public new static ApiResult Conflict(string message = "Conflict occurred")
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = 409
        };
    public new static ApiResult InternalServerError(string message = "An error occurred")
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = 500
        };
}