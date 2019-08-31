namespace NineJoke.Data.Models
{
    using NineJoke.Data.Common.Models;

    public class VoteComment : BaseModel<string>
    {
        public bool UpOrDown { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
