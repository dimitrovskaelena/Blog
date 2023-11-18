using AutoMapper;
using Blog.Domain.Models;
using Blog.Services.Dtos;
using Blog.Services.Implementation;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Reflection.Metadata;
using static System.Reflection.Metadata.BlobBuilder;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<BlogPost> _blogServiceGeneric;
        private readonly IBlogService _blogService;

        public BlogController(IMapper mapper, IGenericService<BlogPost> blogServiceGeneric, IBlogService blogService)
        {
            _mapper = mapper;
            _blogServiceGeneric = blogServiceGeneric;
            _blogService = blogService;
        }

        [HttpGet]
        public IActionResult GetAllBlogs(int page = 1, int pageSize = 10)
        {
            var blogs = _blogServiceGeneric.GetAll(page, pageSize);
            var totalItems = _blogServiceGeneric.GetTotalCount();

            if (blogs == null || !blogs.Any())
            {
                return NotFound();
            }

            return Ok(new { Data = _mapper.Map<List<BlogNode>>(blogs), TotalItems = totalItems });
        }

        [HttpGet("tree")]
        public ActionResult<IEnumerable<BlogPost>> GetTree()
        {
            var blogPosts = _blogServiceGeneric.GetAll(null, null).ToList();

            var treeNodes = _blogService.BuildBlogTree(blogPosts);

            return Ok(treeNodes);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            var blog = _blogServiceGeneric.GetById(id);
            if (blog == null)
            {
                return NotFound($"Blog with ID {id} not found");
            }

            return Ok(blog);
        }

        [HttpPost]
        public ActionResult<BlogPost> CreateBlog([FromBody] BlogPost blog)
        {
            if (blog == null)
            {
                return BadRequest();
            }

            _blogServiceGeneric.Create(blog);

            return CreatedAtAction(nameof(GetBlogById), new { id = blog.Id }, blog);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, [FromBody] BlogPost blog)
        {
            if (blog == null || blog.Id != id)
            {
                return BadRequest();
            }

            var existingBlog = _blogServiceGeneric.GetById(id);

            if (existingBlog == null)
            {
                return NotFound();
            }

            _blogServiceGeneric.Update(blog);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var existingBlog = _blogServiceGeneric.GetById(id);

            if (existingBlog == null)
            {
                return NotFound();
            }

            _blogServiceGeneric.Delete(id);

            return NoContent();
        }

        [HttpGet("{id}/RelatedBlogs")]
        public ActionResult<IEnumerable<BlogPost>> GetRelatedBlogPosts(int id)
        {
            var relatedBlogPosts = _blogService.GetRelatedBlogPosts(id);

            if (relatedBlogPosts == null)
            {
                return NotFound();
            }

            return Ok(relatedBlogPosts);
        }

        [HttpPost("{id}/RelatedBlogs")]
        public ActionResult AddRelatedBlogPost(int id, [FromBody] BlogPost relatedBlog)
        {
            if (relatedBlog == null)
            {
                return BadRequest();
            }

            _blogService.AddRelatedBlogPost(id, relatedBlog);

            return NoContent();
        }

        [HttpPost("{id}/AddRelatedBlogPost/{relatedBlogId}")]
        public IActionResult AddRelatedBlogPost(int id, int relatedBlogId)
        {
            var blogPost = _blogServiceGeneric.GetById(id);
            var relatedBlogPost = _blogServiceGeneric.GetById(relatedBlogId);

            if (blogPost == null || relatedBlogPost == null)
            {
                return NotFound();
            }

            _blogService.AddRelatedBlogPost(id, relatedBlogPost);

            return NoContent();
        }

        [HttpDelete("{id}/RelatedBlogs/{relatedBlogId}")]
        public IActionResult RemoveRelatedBlogPost(int id, int relatedBlogId)
        {
            _blogService.RemoveRelatedBlogPost(id, relatedBlogId);
            return NoContent();
        }
    }
}
