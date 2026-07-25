using Safqat.Api.Models;
using Safqat.Application.Common.Exceptions;
using System.Text.Json;

namespace Safqat.Api.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ConflictException => StatusCodes.Status409Conflict,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ForbiddenException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            ApiResult response = statusCode switch
            {
                StatusCodes.Status404NotFound => ApiResult.NotFound(exception.Message),
                StatusCodes.Status409Conflict => ApiResult.Conflict(exception.Message),
                StatusCodes.Status401Unauthorized => ApiResult.Unauthorized(exception.Message),
                StatusCodes.Status403Forbidden => ApiResult.Forbidden(exception.Message),
                _ => ApiResult.InternalServerError(exception.Message)
            };

            response.StatusCode = statusCode;

            if (context.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
                response.TraceId = context.TraceIdentifier;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
