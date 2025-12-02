using System.Net;
using System.Text.Json;

namespace SmallPostAPI
{
    public class ErrorHandlingMiddleware(RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Unhandled exception occurred: {ex.Message}");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var problem = new
                {
                    status = httpContext.Response.StatusCode,
                    error = "An unexpected error occurred.",
                };

                var json = JsonSerializer.Serialize(problem);
                await httpContext.Response.WriteAsync(json);
            }
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
