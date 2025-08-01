using MediatR;

namespace BlogTalks.Application.Comment.Commands;

public record CreateRequest(string Text, int BlogPostId) : IRequest<CreateResponse>;
