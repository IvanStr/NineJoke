namespace NineJoke.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using NineJoke.Data;
    using NineJoke.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class CommentServiceTests
    {
        [Fact]
        public void CreateCommentShouldCreateCommentAndIncreaseTheCommentCountOnThePost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "CreateCategoryShouldCreateCategoryDB")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var commentService = new CommentService(dbContext);

            var post = new Post
            {
                CommentCount = 4,
            };

            dbContext.Posts.Add(post);
            dbContext.SaveChanges();

            var comment = new Comment
            {
                PostId = post.Id
            };

            commentService.CreateComment(comment);

            var comments = dbContext.Comments.ToList();

            Assert.Single(comments);
            Assert.Equal(5, post.CommentCount);
        }

        [Fact]
        public void DeleteCommentShouldDeleteCommentAndDeleteVotesFromIt()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateCategoryShouldCreateCategoryDB")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var commentService = new CommentService(dbContext);

            var comment = new Comment();
            dbContext.Comments.Add(comment);

            var vote = new VoteComment
            {
                CommentId = comment.Id
            };

            dbContext.VoteComments.Add(vote);
            dbContext.SaveChanges();

            commentService.DeleteComment(comment.Id);

            var comments = dbContext.Comments.ToList();
            var votes = dbContext.VoteComments.ToList();

            Assert.Empty(votes);
            Assert.Empty(comments);
        }

        [Fact]
        public void GetcommentsByPostIdShouldReturnAllCommentsFromPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetcommentsByPostIdShouldReturnAllCommentsFromPost")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var commentService = new CommentService(dbContext);

            var post = new Post();
            dbContext.Posts.Add(post);

            dbContext.Comments.AddRange(new List<Comment>
            {
                new Comment { PostId = post.Id},
                new Comment { PostId = post.Id},
                new Comment { PostId = post.Id},
                new Comment { PostId = post.Id},
            });

            dbContext.SaveChanges();

            var comments = commentService.GetcommentsByPostId(post.Id);

            Assert.Equal(4, comments.Count());
        }

        [Fact]
        public void GetCommentsByUserNameShouldReturnAllCommentsMadeByUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetCommentsByUserNameShouldReturnAllCommentsMadeByUser")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var commentService = new CommentService(dbContext);

            var user = new ApplicationUser();
            user.UserName = "Ivan";
            dbContext.Users.Add(user);

            dbContext.Comments.AddRange(new List<Comment>
            {
                new Comment { UserId = user.Id},
                new Comment { UserId = user.Id},
                new Comment { UserId = user.Id},
                new Comment { UserId = user.Id},
            });

            dbContext.SaveChanges();

            var comments = commentService.GetCommentsByUserName(user.UserName);

            Assert.Equal(4, comments.Count());
        }
    }
}
