namespace NineJoke.Services
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

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

            var category = this.context.Categories.FirstOrDefault(x => x.Name == post.Category.Name);
            category.Popularity++;

            this.context.SaveChanges();
        }

        public IQueryable<Post> GetAll()
        {
            return this.context.Posts.Include(x => x.Category.Name);
        }

        public Post GetPostById(string id)
        {
            return this.context.Posts.Include(x => x.Category).Include(x => x.Comments).ThenInclude(z => z.User).FirstOrDefault(x => x.Id == id);
        }
    }
}
