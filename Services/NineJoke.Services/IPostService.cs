namespace NineJoke.Services
{
    using NineJoke.Data.Models;
    using System.Linq;

    public interface IPostService
    {
        void CreatePost(Post post);

        Post GetPostById(string id);

        void AddImageUrl(string id, string imageUrl);

        IQueryable<Post> GetAll();
    }
}
