namespace NineJoke.Data.Models
{
    using System.Collections.Generic;

    using NineJoke.Data.Common.Models;

    public class Post : BaseModel<string>
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<VotePost>();
            this.ReportPosts = new HashSet<ReportPost>();
        }

        public string Title { get; set; }

        public string FilePath { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public int VoteCount { get; set; }

        public int CommentCount { get; set; }

        public int ReportCount { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<VotePost> Votes { get; set; }

        public ICollection<ReportPost> ReportPosts { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Categoryid { get; set; }
        public Category Category { get; set; }
    }
}
