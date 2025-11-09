using System.Diagnostics;

namespace MovieRatingAPI.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            var method = context.Request.Method;
            var path = context.Request.Path;

            _logger.LogInformation("Incoming {method} request to {path}", method, path);

            await _next(context);

            sw.Stop();
            var status = context.Response.StatusCode;
            _logger.LogInformation("Outgoing {status} for {Method} {Path} in {Elapsed}ms",
                status, method, path, sw.ElapsedMilliseconds);
        }
    }
}
