using MediatR;

namespace BlogTalks.Application.BlogPost.Queries;

public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
{
    public GetByIdHandler()
    {
    }

    public async Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
    {
        // get by Id of BlogPostEntities
        //.. (this part is usually done by a repository or a service)

        // map list to GetAllResponse
        var response = new GetByIdResponse
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
        };

        return response;
    }
}
