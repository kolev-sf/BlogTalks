using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.User.Queries;

public class GetAllHandler : IRequestHandler<GetAllRequest, List<GetAllResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetAllHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<GetAllResponse>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        // get list of BlogPostEntities
        var list = _userRepository.GetAll();

        // map list to GetAllResponse
        var getAllResponseList = new List<GetAllResponse>();
        foreach (var item in list)
        {
            var response = new GetAllResponse
            {
                Id = item.Id,
                Name = item.Name,
                Username = item.Username
            };
            getAllResponseList.Add(response);
        }

        return getAllResponseList;
    }
} 
