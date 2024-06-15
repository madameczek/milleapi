using System.Data;
using System.Net;
using milleapi.Models;

namespace milleapi.Middleware;

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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ExceptionDto dto;
        switch (exception)
        {
            case RowNotInTableException rowNotInTableException:
                _logger.LogInformation("Endpoint: {EndpointName}. Message: {Message}", 
                    context.GetEndpoint(), rowNotInTableException.Message);
                dto = new ExceptionDto(HttpStatusCode.NotFound, rowNotInTableException.Message);
                break;
            case ApplicationException _:
                dto = new ExceptionDto(HttpStatusCode.BadRequest, "Application exception occurred.");
                break;
            default:
                dto = new ExceptionDto(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.");
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)dto.StatusCode;
        await context.Response.WriteAsJsonAsync(dto);
    }
}