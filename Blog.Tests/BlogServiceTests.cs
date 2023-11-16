using Blog.Domain.Data;
using Blog.Domain.Models;
using Blog.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;

namespace Blog.Tests
{
    public class BlogServiceTests
    {
        //[Fact]
        //public void GetAllBlogs_ShouldReturnAllBlogs()
        //{
        //    // Arrange
        //    var blogRepositoryMock = new Mock<Repository<BlogPost>>();
        //    var blogService = new BlogService(blogRepositoryMock.Object);

        //    var expectedBlogs = new List<BlogPost>
        //{
        //    new BlogPost { Id = 1, Title = "Blog 1", Text = "Text 1" },
        //    new BlogPost { Id = 2, Title = "Blog 2", Text = "Text 2" },
        //    // Add more blogs as needed
        //};

        //    blogRepositoryMock.Setup(repo => repo.GetAll()).Returns(expectedBlogs);

        //    // Act
        //    var result = blogService.GetAll();

        //    // Assert
        //    Assert.Equal(expectedBlogs, result);
        //}
    }
}