namespace NineJoke.Services
{
    using NineJoke.Data.Models;

    public interface IVoteService
    {
        void CreatePostVote(VotePost vote);

        void CreateCommentVote(VoteComment vote);
    }
}
