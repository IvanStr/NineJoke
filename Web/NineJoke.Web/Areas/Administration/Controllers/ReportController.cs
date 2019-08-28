namespace NineJoke.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using NineJoke.Services;
    using NineJoke.Web.Areas.Administration.ViewModels.Report;

    public class ReportController : AdministrationController
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public IActionResult ReportList()
        {
            var viewModel = this.reportService.GetAll().Select(x => new ReportListingModel
            {
                Id = x.Id,
                Reason = x.Reason,
                PostId = x.PostId,
                PostTitle = x.Post.Title,
                UploaderId = x.UploaderId,
                UploaderName = x.Uploader.UserName,
                ReporterId = x.ReporterId,
                ReporterName = x.Reporter.UserName,
                CreatedOn = x.CreatedOn,
            }).ToList();

            return this.View(viewModel);
        }

        public IActionResult Delete(string id)
        {
            this.reportService.DeleteReport(id);

            return this.RedirectToAction("ReportList");
        }

        public IActionResult DeleteAll(string id)
        {
            this.reportService.DeleteAllReports();

            return this.RedirectToAction("ReportList");
        }
    }
}
