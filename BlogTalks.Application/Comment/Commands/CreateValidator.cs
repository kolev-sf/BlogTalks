using FluentValidation;

namespace BlogTalks.Application.Comment.Commands;

public class CreateValidator : AbstractValidator<CreateRequest>
{
    public CreateValidator()
    {
        RuleFor(x => x.Text).NotEmpty();
    }
}