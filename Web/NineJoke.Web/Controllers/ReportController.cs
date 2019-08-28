namespace NineJoke.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NineJoke.Services;
    using NineJoke.Web.InputModels;

    public class ReportController : BaseController
    {
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly IReportService reportService;

        public ReportController(IPostService postService, IUserService userService, IReportService reportService)
        {
            this.postService = postService;
            this.userService = userService;
            this.reportService = reportService;
        }

        [Authorize]
        public IActionResult ReportPost()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult ReportPost(ReportInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var post = this.postService.GetPostById(inputModel.Id);
            var reporter = this.userService.GetUserByName(this.User.Identity.Name);

            this.reportService.CreateReport(inputModel.ReportReason, post.Id, post.UserId, reporter.Id);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
