using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Logic.Infrastructure;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.Implementation
{
    public class HistoryLogic : IHistoryLogic
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;

        public HistoryLogic(IUnitOfWork unitOfWork,
            IHistoryRepository historyRepository,
            IUserRepository userRepository,
            IPostRepository postRepository)
        {
            _historyRepository = historyRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public void CreateAppreciationHistory(Guid userId, Guid postId)
        {
            var userModel = _userRepository.Get(userId);
            var postModel = _postRepository.Get(postId);

            if (userModel == null || postModel == null || userModel.Id == postModel.UserId)
            {
                return;
            }

            var historyModel = _historyRepository.GetPostHistory(userId, postId, HistoryActionTypes.AppreciatedPost);

            if (historyModel == null)
            {
                var message = "You reacted to a post by " + postModel.User.FirstName + " " + postModel.User.LastName;

                historyModel = new HistoryModel
                { 
                    Id = Guid.NewGuid(),
                    PostId = postId,
                    Message = message,
                    CreatedOn = DateTime.Now,
                    NumberOfVisitedProfile = 0,
                    Type = HistoryActionTypes.AppreciatedPost,
                    UserId = userId,
                    StringCreatedOn = CalculatePostTimeAgo(DateTime.Now)
                };

                _historyRepository.Add(historyModel);
                _unitOfWork.Save();
            }
        }

        public void CreateCommentHistory(Guid userId, Guid postId)
        {
            var userModel = _userRepository.Get(userId);
            var postModel = _postRepository.Get(postId);

            if (userModel == null || postModel == null || userModel.Id == postModel.UserId)
            {
                return;
            }

            var historyModel = _historyRepository.GetPostHistory(userId, postId, HistoryActionTypes.CommentedOnPost);

            if (historyModel == null)
            {
                var message = "You added a comment to a post by " + postModel.User.FirstName + " " + postModel.User.LastName;

                historyModel = new HistoryModel
                {
                    Id = Guid.NewGuid(),
                    PostId = postId,
                    Message = message,
                    CreatedOn = DateTime.Now,
                    NumberOfVisitedProfile = 0,
                    Type = HistoryActionTypes.CommentedOnPost,
                    UserId = userId,
                    StringCreatedOn = CalculatePostTimeAgo(DateTime.Now)
                };

                _historyRepository.Add(historyModel);
                _unitOfWork.Save();
            }
        }

        public void CreateVisitedProfileHistory(Guid userId, Guid userProfileId)
        {
            var userModel = _userRepository.Get(userId);
            var profileUserModel = _userRepository.Get(userProfileId);

            if (userModel == null || profileUserModel == null || userModel.Id == profileUserModel.Id)
            {
                return;
            }

            var historyModel = _historyRepository.GetVisitedProfileHistory(userId, userProfileId);

            if (historyModel == null)
            {

                var message = "You visited " + profileUserModel.FirstName + " " + profileUserModel.LastName + " profile";

                var model = new HistoryModel
                {
                    Id = Guid.NewGuid(),
                    Message = message,
                    CreatedOn = DateTime.Now,
                    NumberOfVisitedProfile = 1,
                    Type = HistoryActionTypes.VisitedProfile,
                    UserId = userId,
                    UserProfileId = userProfileId,
                    StringCreatedOn = CalculatePostTimeAgo(DateTime.Now)
                };

                _historyRepository.Add(model);
                _unitOfWork.Save();
            }

        }

        public IEnumerable<HistoryModel> GetAllForUser(Guid userId)
        {
            var results = _historyRepository.GetAllForUser(userId);

            foreach (var result in results)
            {
                result.StringCreatedOn = CalculatePostTimeAgo(result.CreatedOn);
            }

            return results;
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
