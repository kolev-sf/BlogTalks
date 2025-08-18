using BlogTalks.Application.Contracts;
using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPost.Queries;

public class GetAllHandler : IRequestHandler<GetAllRequest, GetAllResponse>
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IUserRepository _userRepository;

    public GetAllHandler(IBlogPostRepository blogPostRepository, IUserRepository userRepository)
    {
        _blogPostRepository = blogPostRepository;
        _userRepository = userRepository;
    }

    public async Task<GetAllResponse> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        var (totalCount, blogPosts) = _blogPostRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            request.SearchWord,
            request.Tag
        );

        var userIds = blogPosts.Select(bp => bp.CreatedBy).Distinct().ToList();
        var users = _userRepository.GetUsersByIds(userIds);

        //var userDict = users.ToDictionary(u => u.Id, u => u.Username);
        var blogPostModels = blogPosts.Select(bp => new BlogPostModel
        {
            Id = bp.Id,
            Title = bp.Title,
            Text = bp.Text,
            Tags = bp.Tags,
            CreatorName = users.FirstOrDefault(x => bp.CreatedBy == x.Id)?.Name ?? "Unknown",
        }).ToList();

        var response = new GetAllResponse
        {
            BlogPosts = blogPostModels,
            Metadata = new Metadata
            {
                TotalCount = totalCount,
                PageSize = request.PageSize,
                PageNumber = request.PageNumber,
                TotalPages = (int)System.Math.Ceiling(totalCount / (double)request.PageSize)
            }
        };

        return response;
    }
}
