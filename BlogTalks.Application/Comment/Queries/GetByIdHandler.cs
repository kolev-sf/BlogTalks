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
        var comment = _commentRepository.GetById(request.Id);
        if (comment == null)
            throw new KeyNotFoundException($"Comment with ID {request.Id} not found.");

        return new GetByIdResponse
        {
            Id = comment.Id,
            Text = comment.Text,
            CreatedBy = comment.CreatedBy,
            CreatedAt = comment.CreatedAt
        };
    }
}
