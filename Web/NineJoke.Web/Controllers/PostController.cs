﻿namespace NineJoke.Web.Controllers
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
    using NineJoke.Web.ViewModels;

    public class PostController : BaseController
    {
        private readonly IPostService postService;
        private readonly IImageService imageService;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;
        private readonly ICommentService commentService;

        public PostController(IPostService postService, IImageService imageService, ICategoryService categoryService, IUserService userService, ICommentService commentService)
        {
            this.postService = postService;
            this.imageService = imageService;
            this.categoryService = categoryService;
            this.userService = userService;
            this.commentService = commentService;
        }

        public IActionResult PostDetails(string Id)
        {
            var post = this.postService.GetPostById(Id);

            if (post == null)
            {
                return this.View("MissingPost");
            }

            var model = new PostDetailsViewModel
            {
                Id = post.Id,
                Title = post.Title,
                FilePath = post.FilePath,
                Description = post.Description,
                CategoryName = post.Category.Name,
                VoteCount = post.VoteCount,
                CommentCount = post.CommentCount,
            };

            model.Comments = post.Comments.Select(x => new CommentsViewModel
            {
                Id = x.Id,
                Content = x.Content,
                VoteCount = x.VoteCount,
                CreatedOn = x.CreatedOn,
                UserId = x.UserId,
                UserName = x.User.UserName,
                PostId = x.PostId,
            }).ToList();

            return this.View(model);
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
            if (!this.ModelState.IsValid)
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

        [Authorize]
        [HttpPost]
        public IActionResult AddComment([FromBody] CommentInputModel model)
        {
            var currentUser = this.userService.GetUserByName(this.User.Identity.Name);

            var comment = new Comment
            {
                Content = model.Content,
                UserId = currentUser.Id,
                PostId = model.Id,
                CreatedOn = DateTime.Now,
            };

            this.commentService.CreateComment(comment);
            return new JsonResult(model);
            //return this.RedirectToAction(nameof(this.PostDetails), new { Id = model.Id });
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var post = this.postService.GetPostById(id);

            var model = new PostInputModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(PostInputModel viewModel)
        {
            this.postService.EditPost(viewModel.Title, viewModel.Description, viewModel.Id);

            return this.RedirectToAction("UserPosts", "User");
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            var post = this.postService.GetPostById(id);

            var model = new PostDeleteViewModel
            {
                Id = post.Id,
                Title = post.Title,
                ImageUrl = post.FilePath,
                Description = post.Description,
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(PostDeleteViewModel viewModel)
        {
            var post = this.postService.GetPostById(viewModel.Id);

            this.postService.DeletePost(post.Id);

            return this.RedirectToAction("UserPosts", "User");
        }
    }
}
