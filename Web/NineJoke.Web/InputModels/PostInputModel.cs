namespace NineJoke.Web.InputModels
{
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PostInputModel
    {
        private const int TitleMaxLength = 30;
        private const int TitleMinLength = 5;
        private const int DescriptionMaxLength = 300;
        private const int DescriptionMinLength = 0;
        private const string RequiredField = "\"{0}\" is Required";
        private const string InputLength = "The \"{0}\" must be between {2} and {1} characters.";

        [Required(ErrorMessage = RequiredField)]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = InputLength)]
        public string Title { get; set; }

        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = InputLength)]
        public string Description { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [ValidateFile]
        public IFormFile Image { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string Category { get; set; }

        public ICollection<SelectListItem> Categories { get; set; }

        public string Id { get; set; }
    }
}
