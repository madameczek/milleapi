using System.Data;
using System.Net;
using FluentValidation;
using WebApi.Models;

namespace WebApi.Middleware;

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
            case ValidationException validationException:
                _logger.LogWarning("Validation error: {Errors}", 
                    string.Join(", ", validationException.Errors.Select(e => e.ErrorMessage)));
                dto = new ExceptionDto(
                    HttpStatusCode.BadRequest, 
                    string.Join("; ", validationException.Errors.Select(e => e.ErrorMessage)));
                break;
            
            case RowNotInTableException rowNotInTableException:
                _logger.LogInformation("Resource not found: {Message}", rowNotInTableException.Message);
                dto = new ExceptionDto(HttpStatusCode.NotFound, rowNotInTableException.Message);
                break;
            
            case ApplicationException _:
                _logger.LogError(exception, "Application exception occurred");
                dto = new ExceptionDto(HttpStatusCode.BadRequest, "Application exception occurred.");
                break;
            
            default:
                _logger.LogError(exception, "Unhandled exception occurred");
                dto = new ExceptionDto(HttpStatusCode.InternalServerError, 
                    "Internal server error. Please retry later.");
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)dto.StatusCode;
        await context.Response.WriteAsJsonAsync(dto);
    }
}