using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Logic.Infrastructure;
using GetInTouch.Logic.ViewModels.Appreciation;
using GetInTouch.Logic.ViewModels.Post;
using GetInTouch.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GetInTouch.Logic.Implementation
{
    public class PostLogic : IPostLogic
    {
        private string _imagesDirectory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostRepository _postRepository;
        private readonly IAppreciationLogic _appreciationLogic;
        private readonly ICommentLogic _commentLogic;
        private readonly IAppreciationRepository _appreciationRepository;
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly INotificationRepository _notificationRepository;

        public PostLogic(IUnitOfWork unitOfWork,
            IPostRepository postRepository,
            IAppreciationLogic appreciationLogic,
            ICommentLogic commentLogic,
            IAppreciationRepository appreciationRepository,
            IFriendshipRepository friendshipRepository,
            ICommentRepository commentRepository,
            INotificationRepository notificationRepository)
        {
            _unitOfWork = unitOfWork;
            _postRepository = postRepository;
            _appreciationLogic = appreciationLogic;
            _commentLogic = commentLogic;
            _appreciationRepository = appreciationRepository;
            _friendshipRepository = friendshipRepository;
            _commentRepository = commentRepository;
            _notificationRepository = notificationRepository;
        }

        public void Add(Guid userId, AddPostViewModel viewModel)
        {
            var model = new PostModel
            {
                Id = Guid.NewGuid(),
                Description = viewModel.Description,
                UserId = userId,
                Image = GetImagePath(viewModel.Image),
                CommentsNumber = 0,
                SHaresNumber = 0,
                AppreciationsNumber = 0,
                UploadDate = DateTime.Now,
                NotificationsOn = true,
                UploadTime = CalculatePostTimeAgo(DateTime.Now),
                Appreciations = new List<AppreciationModel>(),
                Comnents = new List<CommentModel>()
            };

            _postRepository.Add(model);
            _unitOfWork.Save();
        }

        public IEnumerable<DetailsPostViewModel> GetPosts(Guid userId)
        {
            var result = new List<DetailsPostViewModel>();
            var posts = _postRepository.Get();

            foreach (var post in posts)
            {
                if (!ShouldDisplayPost(post, post.UserId, userId))
                {
                    continue;
                }

                post.UploadTime = CalculatePostTimeAgo(post.UploadDate);
                post.CommentsNumber = post.Comnents.Count();

                result.Add(new DetailsPostViewModel
                {
                    ShouldDisplayActions = post.UserId == userId,
                    Post = post,
                    AppreciationDetails = _appreciationLogic.GetPostAppreciationInfo(post.Id, userId)
                });
            }

            return result;
        }

        public void AddAppreciation(Guid postId, Guid userId, Emoji emoji)
        {
            if (postId == Guid.NewGuid() || userId == Guid.NewGuid())
            {
                throw new Exception("Could not add appreciation to the post");
            }

            _appreciationLogic.CreateOrUpdateAppreciation(postId, userId, emoji);

            var post = _postRepository.Get(postId);
            post.AppreciationsNumber = post.Appreciations.Where(a => a.Emoji != Emoji.None).Count();

            _unitOfWork.Save();
        }

        public void AddComment(Guid postId, Guid userId, string commentMessage)
        {
            _commentLogic.CreateOrUpdateComment(postId, userId, commentMessage);
            _unitOfWork.Save();
        }

        public IEnumerable<PostReactionInfoModel> GetPostReactionInfoModels(Guid postId, Guid userId)
        {
            var userFriendships = _friendshipRepository.GetAllForUser(userId);
            var reactions = _appreciationRepository.GetAppreciationsForPost(postId);
            var reactionsList = new List<PostReactionInfoModel>();
            
            foreach (var reaction in reactions)
            {
                reactionsList.Add(new PostReactionInfoModel
                {
                    Emoji = reaction.Emoji,
                    FullName = reaction.Sender.FirstName + " " + reaction.Sender.LastName,
                    UserId = reaction.SenderId.Value,
                    IsFriend = userFriendships.Any(x => x.SenderId == reaction.SenderId || x.ReceiverId == reaction.SenderId),
                });
            }

            return reactionsList;
        }

        public void SetImagesDirectory(string directory)
        {
            _imagesDirectory = directory;
        }

        #region Private methods

        private string GetImagePath(IFormFile image)
        {
            string fileName = null;
            fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
            string filePath = Path.Combine(_imagesDirectory, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            return fileName;
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

        private bool ShouldDisplayPost(PostModel post, Guid userPostId, Guid activeUserId)
        {
            if (userPostId == activeUserId)
            {
                return true;
            }

            var numberOfFriendships = _friendshipRepository.GetAllForUser(activeUserId).Count();
            if (numberOfFriendships == 0)
            {
                return true;
            }

            var friendshipModel = _friendshipRepository.Get(userPostId, activeUserId);

            if (post.RestrictViewingType == RestrictViewingType.OnlyMe)
            {
                return false;
            }

            if (post.RestrictViewingType == RestrictViewingType.OnlyCloseFriends)
            {
                if (friendshipModel.ReceiverId == userPostId && friendshipModel.SenderId == activeUserId)
                {
                    return friendshipModel.HasReceiverToCloseFriends;
                }

                if (friendshipModel.ReceiverId == activeUserId && friendshipModel.SenderId == userPostId)
                {
                    return friendshipModel.HasSenderToCloseFriends;
                }
            }

            if (friendshipModel == null)
            {
                return false;
            }

            if (friendshipModel.ReceiverId == userPostId && friendshipModel.SenderId == activeUserId)
            {
                return friendshipModel.FollowsReceiver;
            }

            if (friendshipModel.ReceiverId == activeUserId && friendshipModel.SenderId == userPostId)
            {
                return friendshipModel.FollowsSender;
            }

            return false;
        }

        public PostModel Get(Guid postId)
        {
            return _postRepository.Get(postId);
        }

        public IEnumerable<DetailsPostViewModel> GetPostsForUser(Guid userId)
        {
            var result = new List<DetailsPostViewModel>();
            var posts = _postRepository.GetAllForUser(userId);

            foreach (var post in posts)
            {
                post.UploadTime = CalculatePostTimeAgo(post.UploadDate);
                post.CommentsNumber = post.Comnents.Count();

                result.Add(new DetailsPostViewModel
                {
                    ShouldDisplayActions = true,
                    Post = post,
                    AppreciationDetails = _appreciationLogic.GetPostAppreciationInfo(post.Id, userId)
                });
            }

            return result;
        }

        public void RestrictViewing(Guid postId, RestrictViewingType restrictViewingType)
        {
            var postModel = _postRepository.Get(postId);

            if (postModel == null)
            {
                return;
            }

            postModel.RestrictViewingType = restrictViewingType;
            _unitOfWork.Save();
        }

        public void TurnOnNotifications(Guid postId, bool notificationsOn)
        {
            var postModel = _postRepository.Get(postId);

            if (postModel == null)
            {
                return;
            }

            postModel.NotificationsOn = notificationsOn;
            _unitOfWork.Save();
        }

        public void DeletePost(Guid postId)
        {
            var modelPost = _postRepository.Get(postId);

            _appreciationRepository.RemoveRange(modelPost.Appreciations);
            _commentRepository.RemoveRange(modelPost.Comnents);

            var notifications = _notificationRepository.GetNotificationsForPost(postId);
            foreach (var notification in notifications)
            {
                notification.PostId = null;
            }

            _postRepository.Remove(modelPost);
            _unitOfWork.Save();
        }

        public void Edit(EditPostViewModel viewModel)
        {
            var postModel = _postRepository.Get(viewModel.PostId);

            if (postModel == null)
            {
                return;
            }

            postModel.Description = viewModel.Description;
            postModel.Image = GetImagePath(viewModel.NewImage);

            _unitOfWork.Save();
        }

        #endregion
    }
}
