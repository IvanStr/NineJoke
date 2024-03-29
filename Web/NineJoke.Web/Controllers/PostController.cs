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
        private readonly IVoteService voteService;

        public PostController(
            IPostService postService,
            IImageService imageService,
            ICategoryService categoryService,
            IUserService userService,
            ICommentService commentService,
            IVoteService voteService)
        {
            this.postService = postService;
            this.imageService = imageService;
            this.categoryService = categoryService;
            this.userService = userService;
            this.commentService = commentService;
            this.voteService = voteService;
        }

        public IActionResult PostDetails(string id, string sortType)
        {
            var user = this.userService.GetUserByName(this.User.Identity.Name);
            var post = this.postService.GetPostById(id);
            var voted = 0;

            if (post == null)
            {
                return this.View("MissingPost");
            }

            if (user != null)
            {
                voted = this.voteService.CheckPostVote(post.Id, user.Id);
            }

            var model = new PostDetailsViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Uploader = post.User.UserName,
                CreatedOn = post.CreatedOn,
                FilePath = post.FilePath,
                Description = post.Description,
                CategoryName = post.Category.Name,
                VoteCount = post.VoteCount,
                Voted = voted,
                CommentCount = post.CommentCount,
            };

            if (sortType == null || sortType.Contains(SortType.Popular.ToString()))
            {
                model.Comments = post.Comments.Select(x => new CommentsViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    VoteCount = x.VoteCount,
                    CreatedOn = x.CreatedOn,
                    UserId = x.UserId,
                    UserName = x.User.UserName,
                    PostId = x.PostId,
                }).OrderByDescending(x => x.VoteCount).ToList();
            }
            else if (sortType.Contains(SortType.New.ToString()))
            {
                model.Comments = post.Comments.Select(x => new CommentsViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    VoteCount = x.VoteCount,
                    CreatedOn = x.CreatedOn,
                    UserId = x.UserId,
                    UserName = x.User.UserName,
                    PostId = x.PostId,
                }).OrderBy(x => x.CreatedOn).ToList();
            }

            if (user != null)
            {
                for (int i = 0; i < model.Comments.Count(); i++)
                {
                    model.Comments[i].Voted = this.voteService.CheckCommentVote(model.Comments[i].Id, user.Id);
                }
            }

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
        }

        [Authorize]
        [HttpPost]
        public IActionResult VoteComment([FromBody] VoteInputModel model)
        {
            var currentUser = this.userService.GetUserByName(this.User.Identity.Name);
            var comment = this.commentService.GetById(model.Id);
            var userVote = this.voteService.GetVoteCommentById(comment.Id, currentUser.Id);

            if (model.VoteDown != model.VoteUp && userVote == null)
            {
                var vote = new VoteComment
                {
                    CommentId = model.Id,
                    UserId = currentUser.Id,
                };

                if (model.VoteUp == true)
                {
                    vote.UpOrDown = true;
                }
                else if (model.VoteDown == true)
                {
                    vote.UpOrDown = false;
                }

                this.voteService.CreateCommentVote(vote);
            }
            else if (model.VoteDown != model.VoteUp && userVote != null)
            {
                this.voteService.ChangeCommentVote(userVote, model.VoteUp);
            }
            else if (model.VoteDown == model.VoteUp && userVote != null)
            {
                this.voteService.DeleteCommentVote(userVote);
            }

            return new JsonResult(comment.VoteCount);
        }

        [Authorize]
        [HttpPost]
        public IActionResult VotePost([FromBody] VoteInputModel model)
        {
            var currentUser = this.userService.GetUserByName(this.User.Identity.Name);
            var post = this.postService.GetPostById(model.Id);
            var userVote = this.voteService.GetVotePostById(post.Id, currentUser.Id);

            if (model.VoteDown != model.VoteUp && userVote == null)
            {
                var vote = new VotePost
                {
                    PostId = model.Id,
                    UserId = currentUser.Id,
                };

                if (model.VoteUp == true)
                {
                    vote.UpOrDown = true;
                }
                else if (model.VoteDown == true)
                {
                    vote.UpOrDown = false;
                }

                this.voteService.CreatePostVote(vote);
            }
            else if (model.VoteDown != model.VoteUp && userVote != null)
            {
                this.voteService.ChangePostVote(userVote, model.VoteUp);
            }
            else if (model.VoteDown == model.VoteUp && userVote != null)
            {
                this.voteService.DeletePostVote(userVote);
            }

            return new JsonResult(post.VoteCount.ToString());
        }

        [Authorize]
        public IActionResult DeleteComment(string id)
        {
            this.commentService.DeleteComment(id);

            return this.RedirectToAction("UserComments", "User");
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var post = this.postService.GetPostById(id);

            var allCategories = this.categoryService.GetAll();

            var categories = allCategories.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name,
            }).ToList();

            var model = new PostInputModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Categories = categories,
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(PostInputModel viewModel)
        {
            this.postService.EditPost(viewModel.Category, viewModel.Title, viewModel.Description, viewModel.Id);

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
                Category = post.Category.Name,
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(PostDeleteViewModel viewModel)
        {
            var post = this.postService.GetPostById(viewModel.Id);

            this.postService.DeletePost(viewModel.Id);

            return this.RedirectToAction("UserPosts", "User");
        }
    }
}
