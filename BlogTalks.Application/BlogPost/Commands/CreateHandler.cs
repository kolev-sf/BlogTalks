using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPost.Commands;

public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
{
    private readonly IBlogPostRepository _blogPostRepository;

    public CreateHandler(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        var blogPost = new Domain.Entities.BlogPost
        {
            Title = request.Title,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = 5, // not implemented yet, TODO
            Tags = request.Tags
        };

        // add insert logic here
        _blogPostRepository.Add(blogPost);

        // return Id of created Comment Entity
        return new CreateResponse(blogPost.Id);
    }
}
