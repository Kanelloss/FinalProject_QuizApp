using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class CustomErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomErrorHandlingMiddleware> _logger;

    public CustomErrorHandlingMiddleware(RequestDelegate next, ILogger<CustomErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

            // Handle error status codes after the response is returned
            if (context.Response.StatusCode >= 400 && context.Response.StatusCode < 600 && !context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";

                var errorResponse = context.Response.StatusCode switch
                {
                    StatusCodes.Status400BadRequest => new { Message = "Bad Request. Please check your input and try again." },
                    StatusCodes.Status401Unauthorized => new { Message = "Unauthorized. Please log in to access this resource." },
                    StatusCodes.Status403Forbidden => new { Message = "Forbidden. You do not have permission to access this resource." },
                    StatusCodes.Status404NotFound => new { Message = "Not Found. The resource you are looking for could not be found." },
                    StatusCodes.Status405MethodNotAllowed => new { Message = "Method Not Allowed. Please check the HTTP method you are using." },
                    StatusCodes.Status500InternalServerError => new { Message = "Internal Server Error. Something went wrong on our side. Please try again later." },
                    _ => new { Message = $"An error occurred. HTTP Status Code: {context.Response.StatusCode}" }
                };

                var errorJson = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(errorJson);
            }
        }
        catch (Exception ex)
        {
            // Handle unhandled exceptions
            _logger.LogError(ex, "An unexpected error occurred.");

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var errorResponse = new
                {
                    Message = "An unexpected error occurred. Please try again later.",
                    Details = context.Request.IsLocal() ? ex.Message : null // Provide details only for local requests
                };

                var errorJson = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(errorJson);
            }
            else
            {
                _logger.LogWarning("The response has already started. Unable to modify the response.");
            }
        }
    }
}

public static class HttpRequestExtensions
{
    public static bool IsLocal(this HttpRequest request)
    {
        var connection = request.HttpContext.Connection;

        if (connection.RemoteIpAddress is null || connection.LocalIpAddress is null)
        {
            // Εάν οι διευθύνσεις είναι null, θεωρούμε ότι είναι τοπικό.
            return true;
        }

        // Ελέγχει αν η διεύθυνση είναι το loopback (127.0.0.1 ή ::1)
        if (connection.RemoteIpAddress.Equals(connection.LocalIpAddress) ||
            IPAddress.IsLoopback(connection.RemoteIpAddress))
        {
            return true;
        }

        return false;
    }
}

