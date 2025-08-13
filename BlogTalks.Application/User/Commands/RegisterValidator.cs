using FluentValidation;

namespace BlogTalks.Application.User.Commands;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Username).EmailAddress().WithMessage("Username must be an email");
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Password).MinimumLength(8).WithMessage("Minimum 8 characters");
    }
}