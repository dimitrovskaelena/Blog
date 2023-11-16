using Blog.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Models
{
    public class BlogPost : BaseEntity
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Text is required")]
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set;}
        public DateTime? DeletedOn { get; set;}
        public bool? IsDeleted { get; set; }

        public ICollection<BlogPost> RelatedBlogs { get; set; }
    }
}
