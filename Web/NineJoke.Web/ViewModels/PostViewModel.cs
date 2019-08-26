namespace NineJoke.Web.ViewModels
{
    public class PostViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string FilePath { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public int VoteCount { get; set; }

        public int CommentCount { get; set; }
    }
}
