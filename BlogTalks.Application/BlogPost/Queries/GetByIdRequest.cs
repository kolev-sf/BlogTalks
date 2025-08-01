using MediatR;

namespace BlogTalks.Application.BlogPost.Queries;

public class GetByIdRequest : IRequest<GetByIdResponse>
{
    public int Id { get; set; }
}
