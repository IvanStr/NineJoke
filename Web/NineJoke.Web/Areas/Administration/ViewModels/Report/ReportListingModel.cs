namespace NineJoke.Web.Areas.Administration.ViewModels.Report
{
    using System;

    using NineJoke.Data.Models;

    public class ReportListingModel
    {
        public string Id { get; set; }

        public string Reason { get; set; }

        public string PostId { get; set; }

        public string PostTitle { get; set; }

        public string UploaderId { get; set; }

        public string UploaderName { get; set; }

        public string ReporterId { get; set; }

        public string ReporterName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
