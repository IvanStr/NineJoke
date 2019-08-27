namespace NineJoke.Web.InputModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Http;

    public class ValidateFileAttribute : ValidationAttribute
    {
        private const string FileNotFoundMessage = "File not found.";
        private const string FileExtensionMessage = "The file must be png, jpg, gif or webm";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var supportedTypes = new[] { "jpg", "png", "gif", "webm" };
            var file = value as IFormFile;

            if (file == null)
            {
                return new ValidationResult(FileNotFoundMessage);
            }

            var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
            if (!supportedTypes.Contains(fileExt))
            {
                return new ValidationResult(FileExtensionMessage);
            }

            return ValidationResult.Success;
        }
    }
}
