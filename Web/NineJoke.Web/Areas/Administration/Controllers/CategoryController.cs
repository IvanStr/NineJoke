namespace NineJoke.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using NineJoke.Services;
    using NineJoke.Web.Areas.Administration.InputModel.Category;
    using NineJoke.Web.Areas.Administration.ViewModels.Category;

    public class CategoryController : AdministrationController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }


        public IActionResult CategoryList()
        {
            var viewModel = this.categoryService.GetAll().Select(x => new CategoryListingModel
            {
                Id = x.Id,
                Name = x.Name,
                Popularity = x.Popularity,
            }).ToList();

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryInputModel viewModel)
        {
            this.categoryService.CreateCategory(viewModel.Name);

            return this.RedirectToAction("CategoryList");
        }
    }
}
