namespace NineJoke.Web.ViewModels
{
    using System.Collections.Generic;

    public class PostDetailsViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string FilePath { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public int VoteCount { get; set; }

        public int CommentCount { get; set; }

        public ICollection<CommentsViewModel> Comments { get; set; }
    }
}
