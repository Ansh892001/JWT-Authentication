namespace EmployeeManagement.Api.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException()
        : base("User with this email already exists.")
    {
    }

    public UserAlreadyExistsException(string message)
        : base(message)
    {
    }
}