using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.Infrastructure
{
    public interface INotificationLogic
    {
        IEnumerable<NotificationModel> GetAllForUser(Guid userId);
        void CreateFriendshipNotification(Guid id, Guid receiverId);
        void CreateAppreciationNotification(Guid postId, Guid id);
        int GetNumberOfUnseenNotifications(Guid id);
        void CloseOpenNotifications(Guid id);
    }
}
