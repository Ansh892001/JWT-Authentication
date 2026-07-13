using EmployeeManagement.Api.Helpers;
using EmployeeManagement.Api.Models.Responses;

namespace EmployeeManagement.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
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

    private static async Task HandleExceptionAsync(
    HttpContext context,
    Exception exception)
    {
        var exceptionDetails = ExceptionMapper.GetExceptionDetails(exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exceptionDetails.StatusCode;

        var response = new ErrorResponse
        {
            StatusCode = exceptionDetails.StatusCode,
            Message = exception.Message,
            Timestamp = DateTime.UtcNow,
            TraceId = context.TraceIdentifier
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}