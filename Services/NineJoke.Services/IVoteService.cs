using NineJoke.Data.Models;

namespace NineJoke.Services
{
    public interface IVoteService
    {
        void CreatePostVote(VotePost vote);

        void CreateCommentVote(VoteComment vote);
    }
}
