namespace AgregaDotNet.Features.Posts._Entities
{
    public class Post
    {
        public string? Subject { get; set; }
        public string? Summary { get; set; }
        public string? Url { get; set; }

        public DateTimeOffset LastUpdatedTime;

        public DateTimeOffset PublishDate;
        public List<string>? Authors { get; set; }
        public List<string>? Categories { get; set; }
        public Blog Blog { get; set; }

        public Post()
        {
            Authors = new List<string>();
            Categories = new List<string>();
        }

        public Post(Blog blog) : this()
        {
            this.Blog = blog;
        }
    }
}
