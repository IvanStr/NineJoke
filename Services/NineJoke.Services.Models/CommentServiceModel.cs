using NineJoke.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NineJoke.Data.Models
{
    public class CommentServiceModel : BaseModel<string>
    {
        public CommentServiceModel()
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
