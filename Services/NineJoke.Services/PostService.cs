namespace NineJoke.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using NineJoke.Data;
    using NineJoke.Data.Models;

    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;
        private readonly ICommentService commentService;

        public PostService(ApplicationDbContext context, ICommentService commentService)
        {
            this.context = context;
            this.commentService = commentService;
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

            string a = "asdasd";

            a.EndsWith("asd");

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

        public IQueryable<Post> GetPostsByUserName(string name)
        {
            return this.context.Posts.Where(x => x.User.UserName == name);
        }

        public IQueryable<Post> GetPostsUserComments(string name)
        {
            var comments = this.commentService.GetCommentsByUserName(name);

            List<Post> posts = new List<Post>();

            foreach (var comment in comments)
            {
                var post = this.context.Posts.FirstOrDefault(x => x.Id == comment.PostId);

                if (!posts.Contains(post))
                {
                    posts.Add(post);
                }
            }

            var queryable = posts.AsQueryable();

            return queryable;

            //return this.context.Posts.Where(x => x.User.UserName == name);
        }
    }
}
