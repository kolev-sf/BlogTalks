using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.User.Queries;

public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
{
    private readonly IBlogPostRepository _blogPostRepository;

    public GetByIdHandler(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
    {
        // get by Id of BlogPostEntities
        //.. (this part is usually done by a repository or a service)
        var blogPost = _blogPostRepository.GetById(request.Id);

        // map list to GetAllResponse
        var response = new GetByIdResponse
        {
            Id = blogPost.Id,
            Title = blogPost.Title,
            Text = blogPost.Text,
            CreatorName = "Mile Milevski", //not implemeted yet TODO
            Tags = blogPost.Tags            
        };

        return response;
    }
}
