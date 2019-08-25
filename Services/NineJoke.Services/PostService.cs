namespace NineJoke.Services
{
    using System.Linq;

    using NineJoke.Data;
    using NineJoke.Data.Models;

    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;

        public PostService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddImageUrl(string id, string imageUrl)
        {
            var post = this.context.Posts.FirstOrDefault(x => x.Id == id);

            if (post == null)
            {
                return;
            }

            post.FilePath = imageUrl;

            this.context.SaveChanges();
        }

        public void CreatePost(Post post)
        {
            if (post == null)
            {
                return;
            }

            this.context.Posts.Add(post);
            this.context.SaveChanges();
        }
    }
}
