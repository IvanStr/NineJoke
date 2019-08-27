namespace NineJoke.Services
{
    using System.Linq;

    using NineJoke.Data.Models;

    public interface ICommentService
    {
        void CreateComment(Comment comment);

        IQueryable<Comment> GetCommentsByUserName(string name);
    }
}
