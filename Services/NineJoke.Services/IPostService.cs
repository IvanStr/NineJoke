namespace NineJoke.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using NineJoke.Data.Models;

    public interface IPostService
    {
        void CreatePost(Post post);

        Post GetPostById(string id);

        void AddImageUrl(string id, string imageUrl);

        IQueryable<Post> GetAll();

        IQueryable<Post> GetPostsByUserName(string name);

        IQueryable<Post> GetPostsUserComments(string name);
    }
}
