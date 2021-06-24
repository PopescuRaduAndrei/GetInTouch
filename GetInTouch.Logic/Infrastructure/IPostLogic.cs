using GetInTouch.Logic.ViewModels.Post;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.Infrastructure
{
    public interface IPostLogic
    {
        void SetImagesDirectory(string directory);
        void Add(Guid userId, AddPostViewModel viewModel);
        IEnumerable<DetailsPostViewModel> GetPosts(Guid activeUserId);
        IEnumerable<DetailsPostViewModel> GetPostsForUser(Guid userId);
        void AddAppreciation(Guid postId, Guid id, Emoji emoji);
        void AddComment(Guid postId, Guid id, string commentMessage);
        IEnumerable<PostReactionInfoModel> GetPostReactionInfoModels(Guid postId, Guid userId);
        PostModel Get(Guid postId);
        void RestrictViewing(Guid postId, RestrictViewingType restrictViewingType);
        void TurnOnNotifications(Guid postId, bool v);
        void DeletePost(Guid postId);
        void Edit(EditPostViewModel viewModel);
    }
}
