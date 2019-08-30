namespace NineJoke.Services
{
    using NineJoke.Data;
    using NineJoke.Data.Models;

    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext context;
        private readonly IPostService postService;
        private readonly ICommentService commentService;

        public VoteService(ApplicationDbContext context, IPostService postService, ICommentService commentService)
        {
            this.context = context;
            this.postService = postService;
            this.commentService = commentService;
        }

        public void CreateCommentVote(VoteComment vote)
        {
            this.context.VoteComments.Add(vote);

            var comment = this.commentService.GetById(vote.CommentId);

            if (vote.UpOrDown == true)
            {
                comment.VoteCount++;
            }
            else
            {
                comment.VoteCount--;
            }

            this.context.SaveChanges();
        }

        public void CreatePostVote(VotePost vote)
        {
            this.context.VotePosts.Add(vote);

            var post = this.postService.GetPostById(vote.PostId);

            if (vote.UpOrDown == true)
            {
                post.VoteCount++;
            }
            else
            {
                post.VoteCount--;
            }

            this.context.SaveChanges();
        }
    }
}
