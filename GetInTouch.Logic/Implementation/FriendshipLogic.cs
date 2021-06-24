using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Logic.Infrastructure;
using GetInTouch.Logic.ViewModels.Friendship;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.Implementation
{
    public class FriendshipLogic : IFriendshipLogic
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FriendshipLogic(IFriendshipRepository friendshipRepository,
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork)
        {
            _friendshipRepository = friendshipRepository;
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        public void AcceptFriendship(Guid notificationId)
        {
            var notification = _notificationRepository.GetById(notificationId);

            if (notification.SenderId == Guid.Empty || notification.ReceiverId == Guid.Empty)
            {
                return;
            }

            var friendshipModel = new FriendshipModel
            {
                Id = Guid.NewGuid(),
                ReceiverId = notification.ReceiverId,
                SenderId = notification.SenderId,
                FriendsSince = DateTime.Now,
                FollowsReceiver = true,
                FollowsSender = true,
                HasReceiverToCloseFriends = true,
                HasSenderToCloseFriends = true
            };

            notification.Completed = true;
            
            _friendshipRepository.Add(friendshipModel);
            _unitOfWork.Save();
        }

        public IEnumerable<FriendshipModel> GetAllFriendsForUser(Guid userId)
        {
            return _friendshipRepository.GetAllForUser(userId);
        }

        public IEnumerable<FriendViewModel> GetAllFriendsModels(Guid profileUserId, Guid activeUserId)
        {
            var friendsList = new List<FriendViewModel>();
            var friendships = _friendshipRepository.GetAllForUser(profileUserId);

            foreach (var friendship in friendships)
            {
                var friend = friendship.ReceiverId == profileUserId ? friendship.Sender : friendship.Receiver;
                friendsList.Add(new FriendViewModel
                {
                    FullName = friend.FirstName + " " + friend.LastName,
                    ProfileImage = friend.ProfileImage,
                    UserId = friend.Id,
                    ShouldDisplayAddFriendButton = _friendshipRepository.Get(friend.Id, activeUserId) == null,
                    ShouldDisplaySeeProfileButton = _friendshipRepository.Get(friend.Id, activeUserId) != null,
                    ShouldDisplayUnfriendButton = profileUserId == activeUserId
                });
            }

            return friendsList;
        }

        public void RejectFriendship(Guid notificationId)
        {
            var notification = _notificationRepository.GetById(notificationId);

            if (notification.SenderId == Guid.Empty || notification.ReceiverId == Guid.Empty)
            {
                return;
            }

            notification.Completed = true;

            _unitOfWork.Save();
        }

        public bool CheckFriendship(Guid userId1, Guid userId2)
        {
            return _friendshipRepository.CheckFriendship(userId1, userId2);
        }

        public void RemoveFriendship(Guid userId1, Guid userId2)
        {
            var friendshipModel = _friendshipRepository.Get(userId1, userId2);

            if (friendshipModel != null)
            {
                _friendshipRepository.Remove(friendshipModel);
                _unitOfWork.Save();
            }
        }
    }
}
