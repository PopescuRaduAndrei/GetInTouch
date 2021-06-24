using GetInTouch.Logic.ViewModels.Friendship;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.Infrastructure
{
    public interface IFriendshipLogic
    {
        bool CheckFriendship(Guid userId1, Guid userId2);
        IEnumerable<FriendshipModel> GetAllFriendsForUser(Guid userId);
        void AcceptFriendship(Guid notificationId);
        void RejectFriendship(Guid notificationId);
        void RemoveFriendship(Guid id, Guid profileUserId);
        IEnumerable<FriendViewModel> GetAllFriendsModels(Guid profileUserId, Guid activeUserId);
    }
}
