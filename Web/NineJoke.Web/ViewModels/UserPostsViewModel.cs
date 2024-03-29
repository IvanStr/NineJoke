﻿namespace NineJoke.Web.ViewModels
{
    using System;

    public class UserPostsViewModel
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string FilePath { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public int VoteCount { get; set; }

        public int CommentCount { get; set; }
    }
}
