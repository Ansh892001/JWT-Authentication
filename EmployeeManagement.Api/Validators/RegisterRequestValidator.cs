using EmployeeManagement.Api.Models.Requests;
using FluentValidation;

namespace EmployeeManagement.Api.Validators;

public class RegisterRequestValidator
    : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters.");

        RuleFor(x => x.Password)
            .Matches("[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter.");

        RuleFor(x => x.Password)
            .Matches("[a-z]")
            .WithMessage("Password must contain at least one lowercase letter.");

        RuleFor(x => x.Password)
            .Matches("[0-9]")
            .WithMessage("Password must contain at least one digit.");
    }
}