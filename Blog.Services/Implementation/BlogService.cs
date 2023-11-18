using Blog.Repository;
using Blog.Repository.Interfaces;
using Blog.Services.Interfaces;
using Blog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Blog.Services.Dtos;

namespace Blog.Services.Implementation
{
    public class BlogService : IGenericService<BlogPost>, IBlogService
    {
        private readonly IRepository<BlogPost> _blogRepository;
        public BlogService(IRepository<BlogPost> blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public void Delete(int id)
        {
            var entity = _blogRepository.GetById(id);

            if (entity == null)
            {
                throw new InvalidOperationException($"Blog with ID {entity?.Id} not found.");
            }

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            entity.ModifiedOn = DateTime.UtcNow;

            _blogRepository.SaveChanges();
        }

        public BlogPost GetById(int id)
        {
            var includeProperties = new Expression<Func<BlogPost, object>>[]
            {
                b => b.RelatedBlogs
            };

            return _blogRepository.GetById(id, includeProperties);
        }


        public IEnumerable<BlogPost> GetAll(int? page, int? pageSize)
        {
            Expression<Func<IQueryable<BlogPost>, IOrderedQueryable<BlogPost>>> orderByExpression =
            query => query.OrderByDescending(blog => blog.CreatedOn);

            Func<IQueryable<BlogPost>, IOrderedQueryable<BlogPost>> orderByFunction = orderByExpression.Compile();

            Expression<Func<BlogPost, object>>[] includeRelatedBlogPosts = { b => b.RelatedBlogs };

            return _blogRepository.Get(orderBy: orderByFunction, includeProperties: includeRelatedBlogPosts, page: page, pageSize: pageSize);

        }

        public void Create(BlogPost entity)
        {
            ValidateModel(entity);
            _blogRepository.Create(entity);
            _blogRepository.SaveChanges();
        }

        public void Update(BlogPost entity)
        {
            ValidateModel(entity);

            var existingBlog = _blogRepository.GetById(entity.Id);

            if (existingBlog == null)
            {
                throw new InvalidOperationException($"Blog with ID {entity.Id} not found.");
            }

            existingBlog.Title = entity.Title;
            existingBlog.Text = entity.Text;
            existingBlog.ModifiedOn = DateTime.UtcNow;

            _blogRepository.Update(existingBlog);
            _blogRepository.SaveChanges();
        }

        public int GetTotalCount()
        {
            return _blogRepository.GetTotalCount();
        }

        public IEnumerable<BlogPost> GetRelatedBlogPosts(int id)
        {
            var includeProperties = new Expression<Func<BlogPost, object>>[]
            {
                b => b.RelatedBlogs
            };
            var blogPost = _blogRepository.GetById(id, includeProperties);
            return blogPost.RelatedBlogs;
        }

        public void AddRelatedBlogPost(int id, BlogPost relatedBlog)
        {
            var blogPost = _blogRepository.GetById(id);

            if (blogPost != null)
            {
                if (blogPost.RelatedBlogs == null)
                {
                    blogPost.RelatedBlogs = new List<BlogPost>();
                }

                blogPost.RelatedBlogs.Add(relatedBlog);
                _blogRepository.Update(blogPost);
                _blogRepository.SaveChanges();
            }
        }

        public void RemoveRelatedBlogPost(int id, int relatedBlogId)
        {
            var blogPost = GetById(id);

            if (blogPost != null)
            {
                var relatedBlogToRemove = blogPost.RelatedBlogs?.FirstOrDefault(b => b.Id == relatedBlogId);

                if (relatedBlogToRemove != null)
                {
                    blogPost.RelatedBlogs?.Remove(relatedBlogToRemove);
                    _blogRepository.Update(blogPost);
                    _blogRepository.SaveChanges();
                }
            }
        }

        public List<BlogNode> BuildBlogTree(List<BlogPost> allBlogs)
        {
            var treeNodes = allBlogs
                .Where(blog => blog.RelatedBlogs == null || blog.RelatedBlogs.Count == 0)
                .Select(blog => new BlogNode
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Children = GetChildren(allBlogs, blog.Id)
                })
                .ToList();

            return treeNodes;

        }

        private List<BlogNode> GetChildren(List<BlogPost> blogPosts, int parentId)
        {
            var children = blogPosts
                .Where(blog => blog.RelatedBlogs != null && blog.RelatedBlogs.Any(r => r.Id == parentId))
                .Select(blog => new BlogNode
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Children = GetChildren(blogPosts, blog.Id)
                })
                .ToList();

            return children;
        }
        private void ValidateModel(BlogPost blog)
        {
            var context = new ValidationContext(blog, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(blog, context, results, validateAllProperties: true))
            {
                var validationErrors = string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage));
                throw new ArgumentException($"Validation failed: {validationErrors}");
            }
        }

    }
}