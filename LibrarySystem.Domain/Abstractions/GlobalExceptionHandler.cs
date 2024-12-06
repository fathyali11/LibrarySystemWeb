using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibrarySystem.Domain.Abstractions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger= logger;
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            

            if (httpContext == null || httpContext.Response.HasStarted)
                return false;

            _logger.LogError(exception, "\nthere is a problem\n", exception.Message);

            var response = new ProblemDetails
            {
                Type= "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Status=StatusCodes.Status500InternalServerError,
                Title= "Internal Server Error",
                Detail= "there is a problem"
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var jsonResponse = JsonSerializer.Serialize(response);
            await httpContext.Response.WriteAsync(jsonResponse, cancellationToken);

            return true;
        }
    }
}
