using FixedDemo.Application.User.Commands;
using FluentValidation;

namespace FixedDemo.Application.User.Validations
{
    internal class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("E-Mail address is not valid.");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match.");
        }
    }
}
