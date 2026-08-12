using EmployeeManagement.Api.Exceptions;

namespace EmployeeManagement.Api.Helpers;

public static class ExceptionMapper
{
    private static readonly Dictionary<Type, ExceptionDetails> ExceptionMappings = new()
    {
        {
            typeof(InvalidCredentialsException),
            new ExceptionDetails
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                ErrorCode = "AUTH001",
                LogException = false
            }
        },
        {
            typeof(UserAlreadyExistsException),
            new ExceptionDetails
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorCode = "AUTH002",
                LogException = false
            }
        }
    };

    public static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        if (ExceptionMappings.TryGetValue(exception.GetType(), out var details))
        {
            return details;
        }

        return new ExceptionDetails
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            ErrorCode = "GEN500",
            LogException = true
        };
    }
}