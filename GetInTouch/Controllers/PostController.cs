using GetInTouch.Logic.Infrastructure;
using GetInTouch.Logic.ViewModels.Post;
using GetInTouch.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GetInTouch.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPostLogic _postLogic;
        private readonly IUserLogic _userLogic;
        private readonly IAppreciationLogic _appreciationLogic;
        private readonly ICommentLogic _commentLogic;
        private readonly INotificationLogic _notificationLogic;
        private readonly IFriendshipLogic _friendshipLogic;
        private readonly IHistoryLogic _historyLogic;

        public PostController(IWebHostEnvironment webHostEnvironment,
            UserManager<IdentityUser> userManager,
            IPostLogic postLogic,
            IUserLogic userLogic,
            IAppreciationLogic appreciationLogic,
            ICommentLogic commentLogic,
            INotificationLogic notificationLogic,
            IFriendshipLogic friendshipLogic,
            IHistoryLogic historyLogic)
        {
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _userLogic = userLogic;
            _postLogic = postLogic;
            postLogic.SetImagesDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "Images"));
            _appreciationLogic = appreciationLogic;
            _commentLogic = commentLogic;
            _notificationLogic = notificationLogic;
            _friendshipLogic = friendshipLogic;
            _historyLogic = historyLogic;
        }
        public IActionResult Index()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(userId);

                var model = new PostsViewModel
                {
                    Posts = _postLogic.GetPosts(userModel.Id),
                    Friendships = _friendshipLogic.GetAllFriendsForUser(userModel.Id),
                    ActiveUser = userModel
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add(AddPostViewModel viewModel)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(userId);

                _postLogic.Add(userModel.Id, viewModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult AddAppreciation(Guid postId, Emoji emoji)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(userId);

                _postLogic.AddAppreciation(postId, userModel.Id, emoji);
                _notificationLogic.CreateAppreciationNotification(postId, userModel.Id);
                _historyLogic.CreateAppreciationHistory(userModel.Id, postId);

                var viewModel = _appreciationLogic.GetPostAppreciationInfo(postId, userModel.Id);
                return Json(viewModel);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult AddComment(Guid postId, string commentMessage)
        {
            try
            {
                if (String.IsNullOrEmpty(commentMessage))
                {
                    return Json(false);
                }

                var userId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(userId);

                _postLogic.AddComment(postId, userModel.Id, commentMessage);
                _historyLogic.CreateCommentHistory(userModel.Id, postId);

                var numberOfComments = _commentLogic.GetPostNumberComments(postId);
                return Json(numberOfComments);
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }

        public JsonResult GetPostReactionsOverview(Guid postId)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(userId);

                var reactionsList = _postLogic.GetPostReactionInfoModels(postId, userModel.Id);

                var viewModel = new PostReactionsOverviewViewModel
                {
                    PostId = postId,
                    PostReactionInfoModels = reactionsList,
                    NumberOfAngry = reactionsList.Where(r => r.Emoji == Emoji.Angry).Count(), 
                    NumberOfHaha = reactionsList.Where(r => r.Emoji == Emoji.Haha).Count(), 
                    NumberOfLikes = reactionsList.Where(r => r.Emoji == Emoji.Like).Count(), 
                    NumberOfLoves = reactionsList.Where(r => r.Emoji == Emoji.Love).Count(), 
                    NumberOfSad = reactionsList.Where(r => r.Emoji == Emoji.Sad).Count(), 
                    NumberOfWow = reactionsList.Where(r => r.Emoji == Emoji.Wow).Count(), 
                };

                return new JsonResult(viewModel);
            }
            catch (Exception ex)
            {
                return new JsonResult("");
            }
        }

        [HttpGet]
        public IActionResult GetRestrictViewing(Guid postId)
        {
            try
            {
                var postModel = _postLogic.Get(postId);

                var viewModel = new RestrictViewingViewModel
                {
                    PostId = postId,
                    RestrictViewingType = postModel.RestrictViewingType
                };

                return PartialView("_RestrictViewing", viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult RestrictViewing(RestrictViewingViewModel vm)
        {
            try
            {
                _postLogic.RestrictViewing(vm.PostId, vm.RestrictViewingType);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult TurnOnNotifications(Guid postId)
        {
            try
            {
                _postLogic.TurnOnNotifications(postId, true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult TurnOffNotifications(Guid postId)
        {
            try
            {
                _postLogic.TurnOnNotifications(postId, false);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult DeletePost(Guid postId)
        {
            try
            {
                _postLogic.DeletePost(postId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Edit(Guid postId)
        {
            try
            {
                var postModel = _postLogic.Get(postId);

                var viewModel = new EditPostViewModel
                {
                    PostId = postId,
                    Description = postModel.Description,
                    Image = postModel.Image
                };
                return PartialView("_Edit", viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Edit(EditPostViewModel viewModel)
        {
            try
            {
                _postLogic.Edit(viewModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
