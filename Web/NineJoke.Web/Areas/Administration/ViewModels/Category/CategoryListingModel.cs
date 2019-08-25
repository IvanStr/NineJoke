using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NineJoke.Web.Areas.Administration.ViewModels.Category
{
    public class CategoryListingModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Popularity { get; set; }
    }
}
