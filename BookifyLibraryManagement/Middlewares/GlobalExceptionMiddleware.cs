using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System.Net;

namespace Bookify_Library_mgnt.Middlewares;
public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ProblemDetailsFactory problemDetailsFactory, ILogger<GlobalExceptionMiddleware> logger)
    {
        _problemDetailsFactory = problemDetailsFactory;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionsAsync(context, ex);
        }
    }

    private async Task HandleExceptionsAsync(HttpContext httpContext, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception occurred.");
        (int statusCode, string title) = exception switch
        {
            KeyNotFoundException => (404, "Resource Not Found"),
            UnauthorizedAccessException => (401, "Unauthorized Access"),
            ArgumentException => (400, "Bad Request - Invalid Argument"),
            InvalidOperationException => (500, "Invalid Operation"),
            _ => (500, "Internal Server Error")
        };

        var problem = _problemDetailsFactory.CreateProblemDetails(
            httpContext,
            statusCode: statusCode,
            title: title,
            detail: exception.Message,
            instance: httpContext.Request.Path
        );

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";
        await httpContext.Response.WriteAsJsonAsync(problem);
    }

}
