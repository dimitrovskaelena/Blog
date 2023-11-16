namespace Blog.Services.Dtos
{
    public class BlogNode
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<BlogNode> Children { get; set; } = new List<BlogNode>();
    }
}
