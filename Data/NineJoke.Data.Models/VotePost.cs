namespace NineJoke.Data.Models
{
    using NineJoke.Data.Common.Models;

    public class VotePost : BaseModel<string>
    {
        public bool UpOrDown { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string PostId { get; set; }
        public Post Post { get; set; }
    }
}
