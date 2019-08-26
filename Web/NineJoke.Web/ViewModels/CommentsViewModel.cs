namespace NineJoke.Web.ViewModels
{
    using System;

    public class CommentsViewModel
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string PostId { get; set; }

        public string UserId { get; set; }
    }
}
