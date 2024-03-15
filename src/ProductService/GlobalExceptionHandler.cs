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
        switch (exception)
        {
            case BadRequestException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsync(exception.Message);
                break;
            case NotFoundException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await httpContext.Response.WriteAsync(exception.Message);
                break;
            case ValidationException validationException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsJsonAsync(validationException.Errors);
                break;
            default:
                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = exception.GetType().Name,
                    Title = "An unexpected error occurred",
                    Detail = exception.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                });
                break;
        }

        return true;
    }
}