using FluentValidation;
using Services.Authen.Application.Commands;

namespace Services.Authen.Application.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(6).WithMessage("Username must be at least 6 characters long");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
        }
    }
}
