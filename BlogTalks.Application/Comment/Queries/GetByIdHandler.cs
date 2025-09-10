using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BlogTalks.Application.Comment.Queries;

public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ILogger<GetByIdHandler> _logger;

    public GetByIdHandler(ICommentRepository commentRepository, ILogger<GetByIdHandler> logger)
    {
        _commentRepository = commentRepository;
        _logger = logger;
    }

    public async Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetByIdRequest for Comment with ID: {Id}", request.Id);

        var comment = _commentRepository.GetById(request.Id);
        if (comment == null)
            throw new BlogTalksException($"Comment with Id {request.Id} not found.", HttpStatusCode.NotFound);

        return new GetByIdResponse
        {
            Id = comment.Id,
            Text = comment.Text,
            CreatedBy = comment.CreatedBy,
            CreatedAt = comment.CreatedAt
        };
    }
}
