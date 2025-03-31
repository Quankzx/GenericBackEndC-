using FluentValidation;
using Services.Authen.Application.Commands;

namespace Services.Authen.Application.Validators;

 public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
        RuleFor(x => x.RefreshToken).MinimumLength(20);
    }
}
