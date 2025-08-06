using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.Comment.Commands;

public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IBlogPostRepository _blogPostRepository;

    public CreateHandler(ICommentRepository commentRepository, IBlogPostRepository blogPostRepository)
    {
        _commentRepository = commentRepository;
        _blogPostRepository = blogPostRepository;
    }

    public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        // get list of BlogPostEntities
        var blogPost = _blogPostRepository.GetById(request.BlogPostId);
        if (blogPost == null)
            return null;

        var comment = new Domain.Entities.Comment
        {
            BlogPostId = request.BlogPostId,
            BlogPost = blogPost,
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
