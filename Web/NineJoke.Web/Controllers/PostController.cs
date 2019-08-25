namespace NineJoke.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NineJoke.Common;
    using NineJoke.Data.Models;
    using NineJoke.Services;
    using NineJoke.Web.InputModels;

    public class PostController : BaseController
    {
        private readonly IPostService postService;
        private readonly IImageService imageService;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;

        public PostController(IPostService postService, IImageService imageService, ICategoryService categoryService, IUserService userService)
        {
            this.postService = postService;
            this.imageService = imageService;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var allCategories = this.categoryService.GetAll();

            var categories = allCategories.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name,
            }).ToList();

            var model = new PostInputModel { Categories = categories };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PostInputModel model)
        {
            if (!ModelState.IsValid)
            {
                var allCategories = this.categoryService.GetAll();

                var categories = allCategories.Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name,
                }).ToList();

                var modelList = new PostInputModel { Categories = categories };

                return this.View(modelList);
            }

            var category = this.categoryService.GetCategoryByName(model.Category);

            var user = this.userService.GetUserByName(this.User.Identity.Name);

            var post = new Post
            {
                Title = model.Title,
                Description = model.Description,
                CreatedOn = DateTime.Now,
                Category = category,
                User = user,
            };

            this.postService.CreatePost(post);

            if (model.Image != null)
            {
                var imageUrl = await this.imageService.UploadImage(model.Image, GlobalConstants.POST_PATH_TEMPLATE, post.Title, post.Id);

                this.postService.AddImageUrl(post.Id, imageUrl);
            }

            return this.Redirect("/");
        }
    }
}
