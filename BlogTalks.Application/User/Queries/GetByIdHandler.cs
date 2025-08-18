using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using MediatR;
using System.Net;

namespace BlogTalks.Application.User.Queries;

public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
{
    private readonly IUserRepository _userRepository;

    public GetByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetById(request.Id);
        if (user == null)
            throw new BlogTalksException($"User with ID {request.Id} not found.", HttpStatusCode.NotFound);

        // map list to GetAllResponse
        var response = new GetByIdResponse
        {
            Id = user.Id,
            Name = user.Name,
            Username = user.Username
        };

        return response;
    }
}
