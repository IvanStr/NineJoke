﻿namespace NineJoke.Services
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
        private readonly IReportService reportService;
        private readonly IUserService userService;

        public PostService(ApplicationDbContext context, ICommentService commentService, IReportService reportService, IUserService userService)
        {
            this.context = context;
            this.commentService = commentService;
            this.reportService = reportService;
            this.userService = userService;
        }

        public void AddImageUrl(string id, string imageUrl)
        {
            var post = this.context.Posts
                .FirstOrDefault(x => x.Id == id);

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

            var category = this.context.Categories
                .FirstOrDefault(x => x.Name == post.Category.Name);
            category.Popularity++;

            this.context.SaveChanges();
        }

        public void DeletePost(string id)
        {
            var post = this.GetPostById(id);

            var category = this.context.Categories
                .FirstOrDefault(x => x.Name == post.Category.Name);
            category.Popularity--;

            var reports = this.reportService.GetReportsByPostId(id);
            var votes = this.context.VotePosts.Where(x => x.PostId == post.Id);

            if (reports != null)
            {
                this.context.ReportPosts.RemoveRange(reports);
            }

            if (votes != null)
            {
                this.context.VotePosts.RemoveRange(votes);
            }

            this.context.Posts.Remove(post);
            this.context.SaveChanges();
        }

        public void EditPost(string categoryName, string titel, string description, string id)
        {
            var post = this.GetPostById(id);

            this.context.Categories
                .FirstOrDefault(x => x.Id == post.Categoryid).Popularity--;

            var newCategory = this.context.Categories
                .FirstOrDefault(x => x.Name == categoryName);
            newCategory.Popularity++;

            post.Title = titel;
            post.Description = description;
            post.Category = newCategory;

            this.context.SaveChanges();
        }

        public IQueryable<Post> GetAll(string sort)
        {
            if (sort == null || sort.Equals(SortType.Popular.ToString()))
            {
                return this.context.Posts
                    .OrderByDescending(x => x.VoteCount)
                    .Include(x => x.Category);
            }
            else if (sort.Equals(SortType.New.ToString()))
            {
                return this.context.Posts
                    .OrderByDescending(x => x.CreatedOn)
                    .Include(x => x.Category);
            }

            return null;
        }

        public IQueryable<Post> GetByCategoryId(string id, string sort)
        {
            if (sort == null || sort.Equals(SortType.Popular.ToString()))
            {
                return this.context.Posts
                    .Where(x => x.Categoryid == id)
                    .OrderByDescending(x => x.VoteCount)
                    .Include(x => x.Category);
            }
            else if (sort.Equals(SortType.New.ToString()))
            {
                return this.context.Posts
                    .Where(x => x.Categoryid == id)
                    .OrderByDescending(x => x.CreatedOn)
                    .Include(x => x.Category);
            }

            return null;
        }

        public Post GetPostById(string id)
        {
            return this.context.Posts
                .Include(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.Comments)
                .ThenInclude(z => z.User)
                .Include(x => x.Votes)
                .FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Post> GetPostsByUserName(string name)
        {
            return this.context.Posts
                .Where(x => x.User.UserName == name);
        }

        public IQueryable<Post> GetPostsUserComments(string name)
        {
            var comments = this.commentService.GetCommentsByUserName(name);

            List<Post> posts = new List<Post>();

            foreach (var comment in comments)
            {
                var post = this.context.Posts
                    .FirstOrDefault(x => x.Id == comment.PostId);

                if (!posts.Contains(post))
                {
                    posts.Add(post);
                }
            }

            var queryable = posts.AsQueryable();

            return queryable;
        }
    }
}
