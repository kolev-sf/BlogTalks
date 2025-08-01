using MediatR;

namespace BlogTalks.Application.BlogPost.Queries;

public class GetAllHandler : IRequestHandler<GetAllRequest, List<GetAllResponse>>
{
    public GetAllHandler()
    {
    }

    public async Task<List<GetAllResponse>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        // get list of BlogPostEntities
        //.. (this part is usually done by a repository or a service)

        // map list to GetAllResponse
        var list = new List<GetAllResponse>
        {
            new GetAllResponse
            {
                Id = 1,
                Title = "Sample Blog Post",
                Text = "This is a sample blog post text.",
                CreatorName = "Mile Milevski",
                Tags = new List<string>
                {
                    "Tag1",
                    "Tag2"
                }
            },
            new GetAllResponse
            {
                Id = 2,
                Title = "Another Blog Post",
                Text = "This is another sample blog post text.",
                CreatorName = "Dule Dulevski",
                Tags = new List<string>
                {
                    "Tag3",
                    "Tag4"
                }
            }

        };

        return list;
    }
}
