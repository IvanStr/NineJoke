namespace NineJoke.Services
{
    using NineJoke.Data;
    using NineJoke.Data.Models;

    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext context;

        public CommentService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void CreateComment(Comment comment)
        {
            this.context.Comments.Add(comment);
            this.context.SaveChanges();
        }
    }
}
