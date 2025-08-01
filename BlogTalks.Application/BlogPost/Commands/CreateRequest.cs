using MediatR;

namespace BlogTalks.Application.BlogPost.Commands;

public record CreateRequest(string Title, string Text, List<string> Tags) : IRequest<CreateResponse>;
