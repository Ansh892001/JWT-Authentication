namespace EmployeeManagement.Api.Helpers;

public class ExceptionDetails
{
    public int StatusCode { get; init; }

    public string ErrorCode { get; init; } = string.Empty;

    public bool LogException { get; init; } = true;
}