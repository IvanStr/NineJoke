namespace NineJoke.Data.Models
{
    using System.Collections.Generic;

    using NineJoke.Data.Common.Models;

    public class Comment : BaseModel<string>
    {
        public Comment()
        {
            this.Votes = new HashSet<VoteComment>();
        }

        public string Content { get; set; }

        public int VoteCount { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string PostId { get; set; }
        public Post Post { get; set; }

        public ICollection<VoteComment> Votes { get; set; }
    }
}
