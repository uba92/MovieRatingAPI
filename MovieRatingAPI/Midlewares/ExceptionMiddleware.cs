using MovieRatingAPI.Exceptions;
using System.Net;
using System.Text.Json;

namespace MovieRatingAPI.Midlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate? _next;
        private readonly ILogger<ExceptionMiddleware>? _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpectd error occurred.";

            _logger?.LogError(exception, "Unhandled exception occurred.");

            switch(exception)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;

                case DuplicateTitleException:
                    statusCode = HttpStatusCode.Conflict;
                    message = exception.Message;
                    break;

                default:
                    message = exception.Message;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new
            {
                status = (int)statusCode,
                message
            });

            await context.Response.WriteAsync(result);
        }
    }
}
