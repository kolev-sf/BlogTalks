using FluentValidation;

namespace BlogTalks.Application.BlogPost.Commands;

public class CreateValidator : AbstractValidator<CreateRequest>
{
    public CreateValidator()
    {
        RuleFor(x => x.Text).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
    }
}