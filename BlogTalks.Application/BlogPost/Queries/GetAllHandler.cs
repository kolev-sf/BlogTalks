using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPost.Queries;

public class GetAllHandler : IRequestHandler<GetAllRequest, List<GetAllResponse>>
{
    private readonly IBlogPostRepository _blogPostRepository;

    public GetAllHandler(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<List<GetAllResponse>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        // get list of BlogPostEntities
        var list = _blogPostRepository.GetAll();

        // map list to GetAllResponse
        var getAllResponseList = new List<GetAllResponse>();
        foreach (var item in list)
        {
            var response = new GetAllResponse
            {
                Id = item.Id,
                Title = item.Title,
                Text = item.Text,
                Tags = item.Tags,
                CreatorName = $"user with id {item.Id}" // not implemented yet, TODO
            };
            getAllResponseList.Add(response);
        }

        return getAllResponseList;
    }
} 
