using MediatR;

namespace BlogTalks.Application.User.Queries;

public class GetByIdRequest : IRequest<GetByIdResponse>
{
    public int Id { get; set; }
}
