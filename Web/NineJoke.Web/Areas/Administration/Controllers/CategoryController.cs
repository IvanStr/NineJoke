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

        public IActionResult Edit(string id)
        {
            var category = this.categoryService.GetCategoryById(id);

            var model = new CreateCategoryInputModel
            {
                Id = category.Id,
                Name = category.Name,
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(CreateCategoryInputModel viewModel)
        {
            var category = this.categoryService.GetCategoryById(viewModel.Id);

            this.categoryService.EditCategory(viewModel.Name, viewModel.Id);

            return this.RedirectToAction("CategoryList");
        }

        public IActionResult Delete(string id)
        {
            var category = this.categoryService.GetCategoryById(id);

            var model = new CreateCategoryInputModel
            {
                Id = category.Id,
                Name = category.Name,
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Delete(CreateCategoryInputModel viewModel)
        {
            this.categoryService.DeleteCategory(viewModel.Id);

            return this.RedirectToAction("CategoryList");
        }
    }
}
