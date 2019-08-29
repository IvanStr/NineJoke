namespace NineJoke.Services
{
    using System.Linq;

    using NineJoke.Data.Models;

    public interface ICommentService
    {
        Comment GetById(string id);

        void CreateComment(Comment comment);

        void DeleteComment(string id);

        IQueryable<Comment> GetcommentsByPostId(string id);

        IQueryable<Comment> GetCommentsByUserName(string name);
    }
}
