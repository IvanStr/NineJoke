namespace NineJoke.Services
{
    using System.Linq;

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

        public void ChangeCommentVote(VoteComment vote, bool up)
        {
            if (vote == null)
            {
                return;
            }

            var comment = this.commentService.GetById(vote.CommentId);

            if (up == true)
            {
                vote.UpOrDown = true;
            }
            else
            {
                vote.UpOrDown = false;
            }

            if (vote.UpOrDown == true)
            {
                comment.VoteCount += 2;
            }
            else
            {
                comment.VoteCount -= 2;
            }

            this.context.SaveChanges();
        }

        public void ChangePostVote(VotePost vote, bool up)
        {
            if (vote == null)
            {
                return;
            }

            var post = this.postService.GetPostById(vote.PostId);

            if (up == true)
            {
                vote.UpOrDown = true;
            }
            else
            {
                vote.UpOrDown = false;
            }

            if (vote.UpOrDown == true)
            {
                post.VoteCount += 2;
            }
            else
            {
                post.VoteCount -= 2;
            }

            this.context.SaveChanges();
        }

        public int CheckCommentVote(string commentId, string userId)
        {
            var vote = this.context.VoteComments.FirstOrDefault(x => x.CommentId == commentId && x.UserId == userId);
            if (vote == null)
            {
                return 0;
            }
            else if (vote.UpOrDown == true)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int CheckPostVote(string postId, string userId)
        {
            var vote = this.context.VotePosts.FirstOrDefault(x => x.PostId == postId && x.UserId == userId);
            if (vote == null)
            {
                return 0;
            }
            else if (vote.UpOrDown == true)
            {
                return 1;
            }
            else
            {
                return -1;
            }
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

        public void DeleteCommentVote(VoteComment vote)
        {
            if (vote == null)
            {
                return;
            }

            var comment = this.commentService.GetById(vote.CommentId);

            if (vote.UpOrDown == true)
            {
                comment.VoteCount--;
            }
            else
            {
                comment.VoteCount++;
            }

            this.context.VoteComments.Remove(vote);
            this.context.SaveChanges();
        }

        public void DeletePostVote(VotePost vote)
        {
            if (vote == null)
            {
                return;
            }

            var post = this.postService.GetPostById(vote.PostId);

            if (vote.UpOrDown == true)
            {
                post.VoteCount--;
            }
            else
            {
                post.VoteCount++;
            }

            this.context.VotePosts.Remove(vote);
            this.context.SaveChanges();
        }

        public VoteComment GetVoteCommentById(string commentId, string userId)
        {
            var vote = this.context.VoteComments.FirstOrDefault(x => x.CommentId == commentId && x.UserId == userId);
            return vote;
        }

        public VotePost GetVotePostById(string postId, string userId)
        {
            var vote = this.context.VotePosts.FirstOrDefault(x => x.PostId == postId && x.UserId == userId);
            return vote;
        }
    }
}
