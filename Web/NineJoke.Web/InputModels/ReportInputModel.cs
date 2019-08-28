namespace NineJoke.Web.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class ReportInputModel
    {
        private const int ReportReasonMaxLength = 300;
        private const int ReportReasonMinLength = 5;
        private const string RequiredField = "\"{0}\" is Required";
        private const string InputLength = "The \"{0}\" must be between {2} and {1} characters.";

        [Required(ErrorMessage = RequiredField)]
        [StringLength(ReportReasonMaxLength, MinimumLength = ReportReasonMinLength, ErrorMessage = InputLength)]
        public string ReportReason { get; set; }

        public string Id { get; set; }
    }
}
