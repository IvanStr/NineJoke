namespace NineJoke.Web.Areas.Administration.InputModel.Category
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCategoryInputModel
    {
        private const string NameErrorMessage = "Category Name is Required";
        private const string NameLengthError = "The {0} must be at least {2} and at max {1} characters long.";
        private const int NameMaxLenght = 30;
        private const int NameMinLenght = 3;

        [Required(ErrorMessage = NameErrorMessage)]
        [StringLength(NameMaxLenght, ErrorMessage = NameLengthError, MinimumLength = NameMinLenght)]
        public string Name { get; set; }

        public string Id { get; set; }
    }
}
