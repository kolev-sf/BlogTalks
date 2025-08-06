using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.Comment.Queries;

public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
{
    private readonly ICommentRepository _commentRepository;

    public GetByIdHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
    {
        // get by Id of BlogPostEntities
        var comment = _commentRepository.GetById(request.Id);
        if (comment == null)
            return null;

        // map list to GetAllResponse
        var response = new GetByIdResponse
        {
            Id = comment.Id,
            Text = comment.Text,
            CreatedAt = comment.CreatedAt,
            CreatedBy = 123, // Example user ID
        };

        return response;
    }
}
