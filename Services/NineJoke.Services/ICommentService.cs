namespace NineJoke.Services
{
    using NineJoke.Data.Models;

    public interface ICommentService
    {
        void CreateComment(Comment comment);
    }
}
