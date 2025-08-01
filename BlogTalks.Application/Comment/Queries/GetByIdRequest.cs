using MediatR;

namespace BlogTalks.Application.Comment.Queries;

public class GetByIdRequest : IRequest<GetByIdResponse>
{
    public int Id { get; set; }
}
