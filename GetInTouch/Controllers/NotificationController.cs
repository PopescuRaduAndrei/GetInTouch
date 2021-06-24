using GetInTouch.Logic.Infrastructure;
using GetInTouch.Logic.ViewModels.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetInTouch.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly INotificationLogic _notificationLogic;
        private readonly IUserLogic _userLogic;
        private readonly UserManager<IdentityUser> _userManager;

        public NotificationController(INotificationLogic notificationLogic, IUserLogic userLogic, UserManager<IdentityUser> userManager)
        {
            _notificationLogic = notificationLogic;
            _userLogic = userLogic;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(userId);

                var viewModel = new NotificationsViewModel
                {
                    Notifications = _notificationLogic.GetAllForUser(userModel.Id),
                    ActiveUserId = userModel.Id
                };

                return PartialView("_Index", viewModel);
            }
            catch (Exception ex)
            {
                return PartialView("_Index");
            }
        }

        [HttpPost]
        public JsonResult CreateFriendshipNotification(Guid receiverId)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(userId);

                _notificationLogic.CreateFriendshipNotification(userModel.Id, receiverId);
                return new JsonResult("true");
            }
            catch (Exception ex)
            {
                return new JsonResult("false");
            }
        }

        [HttpPost]
        public IActionResult CloseOpenNotifications()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var userModel = _userLogic.GetUserFromIdentity(userId);

                _notificationLogic.CloseOpenNotifications(userModel.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
