﻿using BlogTalks.Domain.Entities;

namespace BlogTalks.Domain.Repositories
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        BlogPost? GetBlogPostByName(string name);
    }
}
