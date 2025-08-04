using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.Comment.Commands;

public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
{
    private readonly ICommentRepository _commentRepository;

    public CreateHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        var comment = new Domain.Entities.Comment
        {
            BlogPostId = request.BlogPostId,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = 5, // not implemented yet, TODO
        };

        // add insert logic here
        _commentRepository.Add(comment);

        // return Id of created Comment Entity
        return new CreateResponse(comment.Id);
    }
}
