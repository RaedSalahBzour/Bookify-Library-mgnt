using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Service.Exceptions;
using System.Net;

namespace Bookify_Library_mgnt.Middlewares;
public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ProblemDetailsFactory problemDetailsFactory,
        ILogger<GlobalExceptionMiddleware> logger)
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
        ProblemDetails problem;
        int statusCode;
        if (exception is ApiException apiEx)
        {
            statusCode = apiEx.StatusCode;
            problem = _problemDetailsFactory.CreateProblemDetails(
                httpContext,
                statusCode: statusCode,
                title: apiEx.Message,
                detail: apiEx.ErrorDetails ?? apiEx.Message,
                instance: httpContext.Request.Path
            );
        }
        else
        {
            statusCode = (int)HttpStatusCode.InternalServerError;
            problem = _problemDetailsFactory.CreateProblemDetails(
                httpContext,
                statusCode: statusCode,
                title: "Internal Server Error",
                detail: exception.Message,
                instance: httpContext.Request.Path
            );
        }

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";
        await httpContext.Response.WriteAsJsonAsync(problem);
    }

}
