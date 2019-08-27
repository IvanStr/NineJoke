namespace NineJoke.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NineJoke.Services;
    using NineJoke.Web.ViewModels;

    public class UserController : BaseController
    {
        private readonly IPostService postService;
        private readonly ICommentService commentService;

        public UserController(IPostService postService, ICommentService commentService)
        {
            this.postService = postService;
            this.commentService = commentService;
        }

        [Authorize]
        public IActionResult UserPosts()
        {
            string name = this.User.Identity.Name;

            var viewModel = this.postService.GetPostsByUserName(name).Select(x => new PostViewModel
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

        [Authorize]
        public IActionResult UserComments()
        {
            string name = this.User.Identity.Name;

            var viewModel = this.commentService.GetCommentsByUserName(name).Select(x => new CommentsViewModel
            {
                Id = x.Id,
                Content = x.Content,
                VoteCount = x.VoteCount,
                CreatedOn = x.CreatedOn,
                UserId = x.UserId,
                UserName = x.User.UserName,
                PostId = x.PostId,
            }).ToList().OrderByDescending(x => x.CreatedOn);

            return this.View(viewModel);
        }
    }
}
