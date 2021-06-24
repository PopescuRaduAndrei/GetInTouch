using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Logic.Infrastructure;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetInTouch.Logic.Implementation
{
    public class NotificationLogic : INotificationLogic
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IAppreciationRepository _appreciationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFriendshipRepository _friendshipRepository;

        public NotificationLogic (INotificationRepository notificationRepository,
            IUserRepository userRepository,
            IPostRepository postRepository,
            IAppreciationRepository appreciationRepository,
            IUnitOfWork unitOfWork,
            IFriendshipRepository friendshipRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _appreciationRepository = appreciationRepository;
            _unitOfWork = unitOfWork;
            _friendshipRepository = friendshipRepository;
        }

        public void CloseOpenNotifications(Guid userId)
        {
            var notifications = _notificationRepository.GetAllForUser(userId);

            foreach (var notification in notifications)
            {
                notification.Completed = true;
            }

            _unitOfWork.Save();
        }

        public void CreateAppreciationNotification(Guid postId, Guid senderId)
        {
            var postModel = _postRepository.Get(postId);

            if (!postModel.NotificationsOn)
            {
                return;
            }

            if (postModel.UserId == senderId)
            {
                return;
            }

            var postAppreciations = _appreciationRepository.GetAppreciationsForPost(postId)
                .Where(a => a.Emoji != Emoji.None)
                .OrderByDescending(a => a.CreatedOn);
            var notificationModel = _notificationRepository.GetAppreciationNotification(postId);

            var notificationMessage = "";
            
            var lastUserAppreciation = postAppreciations.FirstOrDefault();
            if (lastUserAppreciation == null)
            {
                if (notificationModel != null)
                {
                    _notificationRepository.Remove(notificationModel);
                    _unitOfWork.Save();
                }

                return;
            }

            var userFullName = lastUserAppreciation.Sender.FirstName + " " + lastUserAppreciation.Sender.LastName;
            var numberOfAppreciations = postAppreciations.Count();

            if (numberOfAppreciations == 1)
            {
                notificationMessage = string.Format("{0} liked your photo.", userFullName);
            }
            else
            {
                notificationMessage = string.Format("{0} and {1} others liked your photo.", userFullName, numberOfAppreciations - 1);
            }

            if (notificationModel == null)
            {
                notificationModel = new NotificationModel
                {
                    Id = Guid.NewGuid(),
                    ReceiverId = postModel.UserId,
                    PostId = postId,
                    SenderId = senderId,
                    Type = NotificationTypes.Appreciation,
                    Completed = false,
                    CreatedOn = DateTime.Now,
                    CreateOnString = CalculatePostTimeAgo(DateTime.Now),
                    Message = notificationMessage
                };

                _notificationRepository.Add(notificationModel);
            }
            else
            {
                notificationModel.Message = notificationMessage;
                notificationModel.CreatedOn = DateTime.Now;
                notificationModel.CreateOnString = CalculatePostTimeAgo(DateTime.Now);
                notificationModel.Completed = false;
            }

            _unitOfWork.Save();
        }

        public void CreateFriendshipNotification(Guid senderId, Guid receiverId)
        {
            var model = _notificationRepository.GetFriendshipNotification(senderId, receiverId);
            var friendship = _friendshipRepository.Get(senderId, receiverId);

            if (friendship != null && model != null && !model.Completed)
            {
                return;
            }

            var userModel = _userRepository.Get(senderId);
            var message = string.Format("{0} {1} sent you a friend request.", userModel.FirstName, userModel.LastName);

            model = new NotificationModel
            {
                Id = Guid.NewGuid(),
                ReceiverId = receiverId,
                SenderId = senderId,
                Message = message,
                CreatedOn = DateTime.Now,
                CreateOnString = CalculatePostTimeAgo(DateTime.Now)
            };

            _notificationRepository.Add(model);
            _unitOfWork.Save();
        }

        public IEnumerable<NotificationModel> GetAllForUser(Guid userId)
        {
            var notifications = _notificationRepository.GetAllForUser(userId);

            foreach (var notification in notifications)
            {
                notification.CreateOnString = CalculatePostTimeAgo(notification.CreatedOn);
            }

            return notifications.OrderBy(n => n.Completed).ThenByDescending(n => n.CreatedOn);
        }

        public int GetNumberOfUnseenNotifications(Guid userId)
        {
            var notifications = _notificationRepository.GetAllForUser(userId);
            return notifications.Where(n => !n.Completed).Count();
        }

        private string CalculatePostTimeAgo(DateTime date)
        {
            var difference = DateTime.Now.Subtract(date);
            var totalSeconds = difference.TotalSeconds;

            if (totalSeconds < 60)
            {
                return "Just now";
            }

            if (totalSeconds < 3600)
            {
                var minutes = Math.Truncate(totalSeconds / 60);
                var x = minutes != 1 ? minutes.ToString() + " minutes ago" : minutes.ToString() + " minute ago";
                return x;
            }

            if (totalSeconds < 3600 * 24)
            {
                var hours = Math.Truncate(totalSeconds / 3600);
                return hours != 1 ? hours.ToString() + " hours ago" : hours.ToString() + " hour ago";
            }

            return date.ToString("MMMM d, yyyy");
        }
    }
}
