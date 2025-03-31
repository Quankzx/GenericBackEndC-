using FluentValidation;
using Services.Authen.Application.Commands;

namespace Services.Authen.Application.Validators;

 public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).MinimumLength(6);
        RuleFor(x => x.Email).EmailAddress();
    }
}
