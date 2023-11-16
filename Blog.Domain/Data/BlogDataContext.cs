using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Models;
using System.Reflection.Emit;

namespace Blog.Domain.Data
{
    public class BlogDataContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }

        public BlogDataContext(DbContextOptions<BlogDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BlogPost>()
            .HasMany(b => b.RelatedBlogs)
            .WithMany()  // This creates a many-to-many relationship
            .UsingEntity(j => j.ToTable("RelatedBlogs"));

            SeedData(builder);
            base.OnModelCreating(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            builder.Entity<BlogPost>().HasData(new BlogPost { Id = 1, Title = "Title 1", Text = "Text 1", CreatedOn = DateTime.Now },
                new BlogPost { Id = 2, Title = "Title 2", Text = "Text 2", CreatedOn = DateTime.Now },
                new BlogPost { Id = 3, Title = "Title 3", Text = "Text 3", CreatedOn = DateTime.Now });
        }
    }
}
