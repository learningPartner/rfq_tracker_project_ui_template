using rfq.api.Constants;
using rfq.api.DTOs;
using System.Net;
using System.Text.Json;

namespace rfq.api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode;
        string message;
        List<string>? errors = null;

        switch (exception)
        {
            case ArgumentNullException:
            case ArgumentException:
                statusCode = HttpStatusCode.BadRequest;
                message = MessageConstants.InvalidRequest;
                errors = new List<string> { exception.Message };
                break;

            case KeyNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = MessageConstants.RecordNotFound;
                errors = new List<string> { exception.Message };
                break;

            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                message = MessageConstants.UnauthorizedAccess;
                errors = new List<string> { exception.Message };
                break;

            case InvalidOperationException:
                statusCode = HttpStatusCode.BadRequest;
                message = MessageConstants.OperationFailed;
                errors = new List<string> { exception.Message };
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = MessageConstants.InternalServerError;
                errors = new List<string> { exception.Message };
                break;
        }

        var response = ApiResponse<object>.FailureResponse(message, errors);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var jsonResponse = JsonSerializer.Serialize(response, jsonOptions);
        return context.Response.WriteAsync(jsonResponse);
    }
}
