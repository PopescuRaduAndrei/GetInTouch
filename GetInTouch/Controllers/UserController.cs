using GetInTouch.Logic.Infrastructure;
using GetInTouch.Logic.ViewModels.Friendship;
using GetInTouch.Logic.ViewModels.History;
using GetInTouch.Logic.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetInTouch.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserLogic _userLogic;
        private readonly IFriendshipLogic _friendshipLogic;
        private readonly INotificationLogic _notificationLogic;
        private readonly IPostLogic _postLogic;
        private readonly IHistoryLogic _historyLogic;

        public UserController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IUserLogic userLogic,
            IFriendshipLogic friendshipLogic,
            INotificationLogic notificationLogic,
            IPostLogic postLogic,
            IHistoryLogic historyLogic)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userLogic = userLogic;
            _friendshipLogic = friendshipLogic;
            _notificationLogic = notificationLogic;
            _postLogic = postLogic;
            _historyLogic = historyLogic;
        }

        [Authorize]
        public IActionResult ProfilePage(Guid userId)
        {
            try
            {
                var identityUserId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(identityUserId);
                var profileUserModel = _userLogic.Get(userId);
                var isMyProfile = userModel.Id == userId;

                if (!isMyProfile)
                {
                    _historyLogic.CreateVisitedProfileHistory(userModel.Id, userId);
                }

                var vm = new ProfilePageViewModel
                {
                    Posts = _postLogic.GetPostsForUser(userId),
                    ActiveUser = userModel,
                    IsMyProfile = isMyProfile,
                    AreFriends = isMyProfile ? false : _friendshipLogic.CheckFriendship(userModel.Id, userId),
                    CloseFriends = isMyProfile ? false : _userLogic.CheckCloseFriends(userId, userModel.Id),
                    IsFollowing = isMyProfile ? false : _userLogic.CheckFollowing(userId, userModel.Id),
                    UserDetails = new UserDetailsViewModel
                    {
                        UserId = profileUserModel.Id,
                        Gender = profileUserModel.Gender,
                        Birthday = profileUserModel.Birthday,
                        Colleague = profileUserModel.Colleague,
                        Email = profileUserModel.Email,
                        HighSchool = profileUserModel.HighSchool,
                        IsFrom = profileUserModel.IsFrom,
                        LivesIn = profileUserModel.LivesIn,
                        PhoneNo = profileUserModel.PhoneNo,
                        WorksAt = profileUserModel.WorksAt,
                        FullName = profileUserModel.FirstName + " " + profileUserModel.LastName,
                        ProfileImage = profileUserModel.ProfileImage
                    }
                };

                return View(vm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(userId);

                return RedirectToAction("ProfilePage", new { userId = userModel.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IActionResult EditProfile(UserDetailsViewModel vm)
        {
            try
            {
                _userLogic.UpdateProfile(vm);
                return RedirectToAction("ProfilePage", new { userId = vm.UserId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public JsonResult GetUserInfo()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(userId);

                var vm = new UserInfoViewModel
                {
                    FullName = userModel.FirstName + " " + userModel.LastName,
                    NumberOfUnseenNotifications = _notificationLogic.GetNumberOfUnseenNotifications(userModel.Id)
                };

                return Json(vm);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult AcceptFriendship(Guid notificationId)
        {
            try
            {
                _friendshipLogic.AcceptFriendship(notificationId);
                return RedirectToAction("Index", "Post");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult RejectFriendship(Guid notificationId)
        {
            try
            {
                _friendshipLogic.RejectFriendship(notificationId);
                return RedirectToAction("Index", "Post");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Unfriend(Guid profileUserId)
        {
            try
            {
                var identityUserId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(identityUserId);

                _friendshipLogic.RemoveFriendship(userModel.Id, profileUserId);
                return RedirectToAction("ProfilePage", new { userId = profileUserId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ChangeProfilePicture(Guid userId)
        {
            try
            {
                var identityUserId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(identityUserId);
                var images = _userLogic.GetAllUserImages(userId).ToList();

                var viewModel = new ChangeProfilePictureViewModel
                {
                    UserImages = images,
                    SelectedImage = userModel.ProfileImage,
                    StringImages = string.Join(",", images)
                };

                return PartialView("_ChangeProfilePicture", viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult ChangeProfilePicture(ChangeProfilePictureViewModel vm)
        {
            try
            {
                var identityUserId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(identityUserId);

                _userLogic.SetProfilePicture(userModel.Id, vm.SelectedImage);
                return RedirectToAction("ProfilePage", new { userId = userModel.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IActionResult AddToCloseFriends(Guid userId)
        {
            try
            {
                var identityUserId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(identityUserId);

                _userLogic.AddToCloseFriends(userId, userModel.Id);
                return RedirectToAction("ProfilePage", new { userId = userId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IActionResult RemoveFromCloseFriends(Guid userId)
        {
            try
            {
                var identityUserId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(identityUserId);

                _userLogic.RemoveFromCloseFriends(userId, userModel.Id);
                return RedirectToAction("ProfilePage", new { userId = userId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IActionResult Follow(Guid userId)
        {
            try
            {
                var identityUserId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(identityUserId);

                _userLogic.Follow(userId, userModel.Id);
                return RedirectToAction("ProfilePage", new { userId = userId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IActionResult Unfollow(Guid userId)
        {
            try
            {
                var identityUserId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(identityUserId);

                _userLogic.Unfollow(userId, userModel.Id);
                return RedirectToAction("ProfilePage", new { userId = userId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IActionResult History()
        {
            try
            {
                var identityUserId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(identityUserId);

                var viewModel = new HistoryViewModel
                {
                    Histories = _historyLogic.GetAllForUser(userModel.Id)
                };

                return PartialView("_History", viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IActionResult FindPeople(string name)
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Friends(Guid userId)
        {
            try
            {
                var identityUserId = _userManager.GetUserId(User);
                var activeUser = _userLogic.GetUserFromIdentity(identityUserId);

                var viewModel = new FriendsViewModel
                {
                    ProfileUserId = userId,
                    ActiveUser = activeUser,
                    Friends = _friendshipLogic.GetAllFriendsModels(userId, activeUser.Id)
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult FilterPeople(Guid profileUserId, string filter)
        {
            try
            {
                var identityUserId = _userManager.GetUserId(User);
                var activeUser = _userLogic.GetUserFromIdentity(identityUserId);

                var friends = _friendshipLogic.GetAllFriendsModels(profileUserId, activeUser.Id);

                if (!String.IsNullOrEmpty(filter))
                {
                    friends = friends.Where(f => f.FullName.ToLower().Contains(filter.ToLower()));
                }

                var viewModel = new FriendsViewModel
                {
                    ProfileUserId = profileUserId,
                    ActiveUser = activeUser,
                    Friends = friends
                };

                return PartialView("_PeopleList", viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userId = _userManager.GetUserId(User);

                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("Could not log out of this account...");
                }

                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Post");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
