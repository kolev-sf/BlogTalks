﻿using MediatR;

namespace BlogTalks.Application.Comment.Queries;

public record GetByBlogPostIdRequest(int BlogPostId) : IRequest<List<GetByBlogPostIdResponse>>;
