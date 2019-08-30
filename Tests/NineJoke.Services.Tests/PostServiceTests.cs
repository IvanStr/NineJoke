namespace NineJoke.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NineJoke.Data;
    using NineJoke.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class PostServiceTests
    {
        [Fact]
        public void AddImageUrlShouldAddImageUrlToPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "AddImageUrlShouldAddImageUrlToPost")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var commentService = new Mock<ICommentService>();
            var reportService = new Mock<IReportService>();
            var userService = new Mock<IUserService>();

            var postService = new PostService(dbContext, commentService.Object, reportService.Object, userService.Object);

            var post = new Post();
            string imgUrl = "asdasdasd";

            dbContext.Posts.Add(post);
            dbContext.SaveChanges();

            postService.AddImageUrl(post.Id, imgUrl);

            Assert.Equal(imgUrl, post.FilePath);
        }

        [Fact]
        public void CreatePostShouldCreatePostAndIncreasePopularityOnCategory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "CreatePostShouldCreatePostAndIncreasePopularityOnCategory")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var commentService = new Mock<ICommentService>();
            var reportService = new Mock<IReportService>();
            var userService = new Mock<IUserService>();

            var postService = new PostService(dbContext, commentService.Object, reportService.Object, userService.Object);

            var category = new Category
            {
                Name = "funny",
                Popularity = 5,
            };

            dbContext.Categories.Add(category);

            var post = new Post
            {
                Categoryid = category.Id,
            };

            dbContext.SaveChanges();

            postService.CreatePost(post);

            var posts = dbContext.Posts.ToList();

            Assert.Single(posts);
            Assert.Equal(6, category.Popularity);
        }

        [Fact]
        public void DeletePostShouldDeletePostAndReportsToThatPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "DeletePostShouldDeletePostAndReportsToThatPost")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var commentService = new Mock<ICommentService>();
            var reportService = new Mock<IReportService>();
            var userService = new Mock<IUserService>();

            var category = new Category();
            dbContext.Categories.Add(category);

            var post = new Post
            {
                Categoryid = category.Id,
            };

            dbContext.Posts.Add(post);

            dbContext.ReportPosts.AddRange(new List<ReportPost>
            {
                new ReportPost { PostId = post.Id},
                new ReportPost { PostId = post.Id},
                new ReportPost { PostId = post.Id},
                new ReportPost { PostId = post.Id},
            });
            dbContext.SaveChanges();

            var reports = dbContext.ReportPosts;

            reportService.Setup(x => x.GetReportsByPostId(post.Id))
                         .Returns(reports);

            var postService = new PostService(dbContext, commentService.Object, reportService.Object, userService.Object);

            postService.DeletePost(post.Id);

            var posts = dbContext.Posts.ToList();
            var reportsTest = dbContext.ReportPosts.ToList();

            Assert.Empty(posts);
            Assert.Empty(reportsTest);
        }

        [Fact]
        public void EditPostShouldEditCategoryTitleAndDescription()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "EditPostShouldEditCategoryTitleAndDescription")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var commentService = new Mock<ICommentService>();
            var reportService = new Mock<IReportService>();
            var userService = new Mock<IUserService>();

            var postService = new PostService(dbContext, commentService.Object, reportService.Object, userService.Object);

            var category = new Category
            {
                Name = "funny",
                Popularity = 3,
            };

            var category2 = new Category
            {
                Name = "NotFunny",
                Popularity = 3,
            };
            dbContext.Categories.Add(category);
            dbContext.Categories.Add(category2);

            var post = new Post
            {
                Categoryid = category.Id,
                Title = "OldTitle",
                Description = "OldDesc",
            };
            dbContext.Posts.Add(post);
            dbContext.SaveChanges();

            postService.EditPost(category2.Name, "NewTitle", "NewDesc", post.Id);

            Assert.Equal(post.Category.Name, category2.Name);
            Assert.Equal("NewTitle", post.Title);
            Assert.Equal("NewDesc", post.Description);
            Assert.Equal(2, category.Popularity);
            Assert.Equal(4, category2.Popularity);
        }

        [Fact]
        public void GetAllShouldReturnAllPostSorted()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "GetAllShouldReturnAllPostSorted")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var commentService = new Mock<ICommentService>();
            var reportService = new Mock<IReportService>();
            var userService = new Mock<IUserService>();

            var postService = new PostService(dbContext, commentService.Object, reportService.Object, userService.Object);

            var category = new Category
            {
                Name = "funny",
                Popularity = 3,
            };
            dbContext.Categories.Add(category);

            dbContext.Posts.AddRange(new List<Post>
            {
                new Post { CreatedOn = DateTime.Now, VoteCount = 4, Categoryid = category.Id},
                new Post { CreatedOn = DateTime.Now, VoteCount = 3, Categoryid = category.Id},
                new Post { CreatedOn = DateTime.Now, VoteCount = 2, Categoryid = category.Id},
                new Post { CreatedOn = DateTime.Now, VoteCount = 1, Categoryid = category.Id},
            });
            dbContext.SaveChanges();

            var popularSort = postService.GetAll("Popular").First();
            var newSort = postService.GetAll("New").First();

            Assert.Equal(4, popularSort.VoteCount);
            Assert.Equal(1, newSort.VoteCount);
        }
    }
}
