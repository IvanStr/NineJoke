﻿namespace NineJoke.Services
{
    using System.Linq;

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

            var post = this.context.Posts.FirstOrDefault(x => x.Id == comment.PostId);
            post.CommentCount++;

            this.context.SaveChanges();
        }

        public IQueryable<Comment> GetcommentsByPostId(string id)
        {
            return this.context.Comments.Where(x => x.PostId == id);
        }

        public IQueryable<Comment> GetCommentsByUserName(string name)
        {
            return this.context.Comments.Where(x => x.User.UserName == name);
        }
    }
}
