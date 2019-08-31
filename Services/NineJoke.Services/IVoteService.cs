namespace NineJoke.Services
{
    using NineJoke.Data.Models;

    public interface IVoteService
    {
        VotePost GetVotePostById(string postId, string userId);

        void ChangePostVote(VotePost vote, bool up);

        int CheckPostVote(string postId, string userId);

        void DeletePostVote(VotePost vote);

        void CreatePostVote(VotePost vote);

        VoteComment GetVoteCommentById(string commentId, string userId);

        void ChangeCommentVote(VoteComment vote, bool up);

        int CheckCommentVote(string commentId, string userId);

        void CreateCommentVote(VoteComment vote);

        void DeleteCommentVote(VoteComment vote);
    }
}
