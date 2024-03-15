using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductService.Common.Exceptions;

namespace ProductService;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is BadRequestException or ValidationException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsync(exception.Message);
            return true;
        }

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Type = exception.GetType().Name,
            Title = "An unexpected error occurred",
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        });

        return true;
    }
}