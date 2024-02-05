using System.Net.Mime;

namespace BuildingLink.App.Middleware
{
    public class UnhandledExceptionCatchingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UnhandledExceptionCatchingMiddleware> _logger;

        public UnhandledExceptionCatchingMiddleware(
            RequestDelegate next,
            ILogger<UnhandledExceptionCatchingMiddleware> logger)
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
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred. Trace Identifier: {TraceIdentifier}", context.TraceIdentifier);

            try
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                switch (ex)
                {
                    case InvalidOperationException:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }

                return context.Response.WriteAsync(ex.Message);
            }
            catch(Exception exception) 
            {
                _logger.LogError(exception, "There was an exception creating the error payload response");
            }

            return Task.CompletedTask;
        }
    }
}
