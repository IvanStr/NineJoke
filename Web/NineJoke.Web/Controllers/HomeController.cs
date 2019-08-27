namespace NineJoke.Web.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using NineJoke.Services;
    using NineJoke.Web.ViewModels;

    public class HomeController : BaseController
    {
        private readonly IPostService postService;

        public HomeController(IPostService postService)
        {
            this.postService = postService;
        }

        public IActionResult Index()
        {
            var viewModel = this.postService.GetAll().Select(x => new PostViewModel
            {
                Id = x.Id,
                CreatedOn = x.CreatedOn,
                Title = x.Title,
                FilePath = x.FilePath,
                CategoryName = x.Category.Name,
                Description = x.Description,
                VoteCount = x.VoteCount,
                CommentCount = x.CommentCount,
            }).ToList().OrderByDescending(x => x.CreatedOn);

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
