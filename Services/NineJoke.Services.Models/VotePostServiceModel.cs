using NineJoke.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NineJoke.Data.Models
{
    public class VotePostServiceModel : BaseModel<string>
    {
        public int UpOrDown { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string PostId { get; set; }
        public Post Post { get; set; }
    }
}
