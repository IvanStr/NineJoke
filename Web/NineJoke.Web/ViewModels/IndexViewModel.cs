namespace NineJoke.Web.ViewModels
{
    using System.Collections.Generic;

    using NineJoke.Data.Models;

    public class IndexViewModel
    {
        public IList<Category> Categories { get; set; }

        public IList<PostViewModel> PostViewModel { get; set; }
    }
}
