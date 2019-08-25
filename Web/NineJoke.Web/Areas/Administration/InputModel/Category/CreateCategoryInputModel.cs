using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NineJoke.Web.Areas.Administration.InputModel.Category
{
    public class CreateCategoryInputModel
    {
        private const string NameErrorMessage = "Category Name is Required";

        [Required(ErrorMessage = NameErrorMessage)]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; }
    }
}
