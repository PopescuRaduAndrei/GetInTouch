using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Logic.Infrastructure;
using GetInTouch.Logic.ViewModels.User;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetInTouch.Logic.Implementation
{
    public class UserLogic : IUserLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IFriendshipRepository _friendshipRepository;

        public UserLogic(IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IPostRepository postRepository,
            IFriendshipRepository friendshipRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _friendshipRepository = friendshipRepository;
        }

        public UserModel Get(Guid userId)
        {
            return _userRepository.Get(userId);
        }

        public UserModel GetUserFromIdentity(string userId)
        {
            return _userRepository.GetUserFromIdentity(userId);
        }

        public void Register(string userId, string firstName, string lastName, string email, int year, int month, int day)
        {
            var birthDayFormat = true;
            var birthDay = new DateTime();

            if (year < 1000 || year > 9999)
            {
                birthDayFormat = false;
            }

            if (month < 1 || month > 12)
            {
                birthDayFormat = false;
            }

            if (day < 1 || day > 31)
            {
                birthDayFormat = false;
            }

            if (birthDayFormat)
            {
                birthDay = new DateTime(year, month, day);
            }

            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Birthday = birthDay
            };

            _userRepository.Add(model);
            _unitOfWork.Save();
        }

        public void UpdateProfile(UserDetailsViewModel vm)
        {
            var userModel = _userRepository.Get(vm.UserId);

            if (userModel == null)
            {
                return;
            }

            userModel.LivesIn = vm.LivesIn;
            userModel.IsFrom = vm.IsFrom;
            userModel.Birthday = vm.Birthday;
            userModel.WorksAt = vm.WorksAt;
            userModel.Colleague = vm.Colleague;
            userModel.HighSchool = vm.HighSchool;
            userModel.Email = vm.Email;
            userModel.PhoneNo = vm.PhoneNo;
            userModel.Gender = vm.Gender;

            _unitOfWork.Save();
        }

        public IEnumerable<string> GetAllUserImages(Guid userId)
        {
            var postsList = _postRepository.GetAllForUser(userId);
            var imagesList = new List<string>();

            foreach (var post in postsList)
            {
                if (!String.IsNullOrEmpty(post.Image)) 
                {
                    imagesList.Add(post.Image);
                }
            }

            return imagesList;
        }

        public void SetProfilePicture(Guid userId, string image)
        {
            var userModel = _userRepository.Get(userId);
            userModel.ProfileImage = image;
            _unitOfWork.Save();
        }

        public void AddToCloseFriends(Guid receiverId, Guid senderId)
        {
            var friendshipModel = _friendshipRepository.Get(receiverId, senderId);

            if (friendshipModel == null)
            {
                return;
            }

            if (friendshipModel.ReceiverId == senderId && friendshipModel.SenderId == receiverId)
            {
                friendshipModel.HasSenderToCloseFriends = true;
            }

            if (friendshipModel.ReceiverId == receiverId && friendshipModel.SenderId == senderId)
            {
                friendshipModel.HasReceiverToCloseFriends = true;
            }

            _unitOfWork.Save();
        }

        public void RemoveFromCloseFriends(Guid receiverId, Guid senderId)
        {
            var friendshipModel = _friendshipRepository.Get(receiverId, senderId);

            if (friendshipModel == null)
            {
                return;
            }

            if (friendshipModel.ReceiverId == senderId && friendshipModel.SenderId == receiverId)
            {
                friendshipModel.HasSenderToCloseFriends = false;
            }

            if (friendshipModel.ReceiverId == receiverId && friendshipModel.SenderId == senderId)
            {
                friendshipModel.HasReceiverToCloseFriends = false;
            }

            _unitOfWork.Save();
        }

        public bool CheckCloseFriends(Guid receiverId, Guid senderId)
        {
            var friendshipModel = _friendshipRepository.Get(receiverId, senderId);

            if (friendshipModel == null)
            {
                return false;
            }

            if (friendshipModel.ReceiverId == senderId && friendshipModel.SenderId == receiverId)
            {
                return friendshipModel.HasSenderToCloseFriends;
            }

            if (friendshipModel.ReceiverId == receiverId && friendshipModel.SenderId == senderId)
            {
                return friendshipModel.HasReceiverToCloseFriends;
            }

            return false;
        }

        public bool CheckFollowing(Guid receiverId, Guid senderId)
        {
            var friendshipModel = _friendshipRepository.Get(receiverId, senderId);

            if (friendshipModel == null)
            {
                return false;
            }

            if (friendshipModel.ReceiverId == senderId && friendshipModel.SenderId == receiverId)
            {
                return friendshipModel.FollowsSender;
            }

            if (friendshipModel.ReceiverId == receiverId && friendshipModel.SenderId == senderId)
            {
                return friendshipModel.FollowsReceiver;
            }

            return false;
        }

        public void Follow(Guid receiverId, Guid senderId)
        {
            var friendshipModel = _friendshipRepository.Get(receiverId, senderId);

            if (friendshipModel == null)
            {
                return;
            }

            if (friendshipModel.ReceiverId == senderId && friendshipModel.SenderId == receiverId)
            {
                friendshipModel.FollowsSender = true;
            }

            if (friendshipModel.ReceiverId == receiverId && friendshipModel.SenderId == senderId)
            {
                friendshipModel.FollowsReceiver = true;
            }

            _unitOfWork.Save();
        }

        public void Unfollow(Guid receiverId, Guid senderId)
        {
            var friendshipModel = _friendshipRepository.Get(receiverId, senderId);

            if (friendshipModel == null)
            {
                return;
            }

            if (friendshipModel.ReceiverId == senderId && friendshipModel.SenderId == receiverId)
            {
                friendshipModel.FollowsSender = false;
            }

            if (friendshipModel.ReceiverId == receiverId && friendshipModel.SenderId == senderId)
            {
                friendshipModel.FollowsReceiver = false;
            }

            _unitOfWork.Save();
        }
    }
}
