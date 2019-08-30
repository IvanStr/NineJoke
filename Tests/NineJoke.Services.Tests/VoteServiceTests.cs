namespace NineJoke.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NineJoke.Data;
    using NineJoke.Data.Models;
    using Xunit;

    public class VoteServiceTests
    {
        [Fact]
        public void CreatePostVoteShouldAddVoteOnPostAndIncreaseOrDecreaseVoteCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: "CreatePostVoteShouldAddVoteOnPostAndIncreaseOrDecreaseVoteCountDB")
                  .Options;

            var dbContext = new ApplicationDbContext(options);

            var commentService = new Mock<ICommentService>();

            var postId = "asdasdasd";

            var post = new Post
            {
                Id = postId,
                VoteCount = 5,
            };

            var postService = new Mock<IPostService>();
            postService.Setup(x => x.GetPostById(postId)).Returns(post);


            var vote = new VotePost
            {
                PostId = post.Id,
                UpOrDown = false,
            };

            var voteService = new VoteService(dbContext, postService.Object, commentService.Object);

            voteService.CreatePostVote(vote);

            Assert.Equal(4, post.VoteCount);
        }

        [Fact]
        public void CreateCommentVoteShouldAddVoteOnCommentAndIncreaseOrDecreaseVoteCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: "CreateCommentVoteShouldAddVoteOnCommentAndIncreaseOrDecreaseVoteCountDB")
                  .Options;

            var dbContext = new ApplicationDbContext(options);

            var postService = new Mock<IPostService>();

            var commentId = "asdasdasd";

            var comment = new Comment
            {
                Id = commentId,
                VoteCount = 7,
            };
            var commentService = new Mock<ICommentService>();
            commentService.Setup(x => x.GetById(commentId)).Returns(comment);

            var vote = new VoteComment
            {
                CommentId = comment.Id,
                UpOrDown = true,
            };

            var voteService = new VoteService(dbContext, postService.Object, commentService.Object);

            voteService.CreateCommentVote(vote);

            Assert.Equal(8, comment.VoteCount);
        }
    }
}
