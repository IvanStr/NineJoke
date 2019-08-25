namespace NineJoke.Services
{
    using NineJoke.Data.Models;

    public interface IPostService
    {
        void CreatePost(Post post);

        void AddImageUrl(string id, string imageUrl);
    }
}
