using BlogTalks.Application.Comment.Queries;
using MediatR;

namespace BlogTalks.Application.BlogPost.Queries;

public class GetByBlogPostIdHandler : IRequestHandler<GetByBlogPostIdRequest, List<GetByBlogPostIdResponse>>
{
    public GetByBlogPostIdHandler()
    {
    }

    public async Task<List<GetByBlogPostIdResponse>> Handle(GetByBlogPostIdRequest request, CancellationToken cancellationToken)
    {
        // get list of BlogPostEntities
        //.. (this part is usually done by a repository or a service)

        // map list to GetAllResponse
        var list = new List<GetByBlogPostIdResponse>
        {
            new GetByBlogPostIdResponse
            {
                Id = 1,
                Text = "This is a sample blog post text.",
                CreatorName = "Mile Milevski",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1 // Example user ID
            },
            new GetByBlogPostIdResponse
            {
                Id = 2,
                Text = "This is another sample blog post text.",
                CreatorName = "Dule Dulevski",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1 // Example user ID
            }
        };

        return list;
    }
}
