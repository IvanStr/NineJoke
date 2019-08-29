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
        private readonly ICategoryService categoryService;

        public HomeController(IPostService postService, ICategoryService categoryService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
        }

        public IActionResult Index(string id, string sort)
        {
            var indexViewModel = new IndexViewModel();

            if (id == null)
            {
                var allPostsViewModel = this.postService.GetAll(sort).Select(x => new PostViewModel
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    Title = x.Title,
                    FilePath = x.FilePath,
                    CategoryName = x.Category.Name,
                    Description = x.Description,
                    VoteCount = x.VoteCount,
                    CommentCount = x.CommentCount,
                }).ToList();

                indexViewModel.PostViewModel = allPostsViewModel.ToList();
            }
            else
            {
                var viewModel = this.postService.GetByCategoryId(id).Select(x => new PostViewModel
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    Title = x.Title,
                    FilePath = x.FilePath,
                    CategoryName = x.Category.Name,
                    Description = x.Description,
                    VoteCount = x.VoteCount,
                    CommentCount = x.CommentCount,
                }).ToList();

                indexViewModel.PostViewModel = viewModel.ToList();
            }

            indexViewModel.sortType = sort;
            indexViewModel.Categories = this.categoryService.GetAll().OrderBy(x => x.Name).ToList();


            return this.View(indexViewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
