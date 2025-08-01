using MediatR;

using BlogTalks.Application.Comment.Queries;

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
            Text = "This is a sample blog post text.",
            CreatedAt = DateTime.UtcNow,
            CreatedBy = 123, // Example user ID
        };

        return response;
    }
}
