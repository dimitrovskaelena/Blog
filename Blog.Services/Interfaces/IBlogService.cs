using Blog.Domain.Models;
using Blog.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Interfaces
{
    public interface IBlogService
    {
        IEnumerable<BlogPost> GetRelatedBlogPosts(int id);
        void AddRelatedBlogPost(int id, BlogPost relatedBlog);
        void RemoveRelatedBlogPost(int id, int relatedBlogId);
        List<BlogNode> BuildBlogTree(IEnumerable<BlogPost> allBlogs);
    }
}
