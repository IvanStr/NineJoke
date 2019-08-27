namespace NineJoke.Web.ViewModels
{
    using System;

    public class CommentsViewModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public int VoteCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public string PostId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
