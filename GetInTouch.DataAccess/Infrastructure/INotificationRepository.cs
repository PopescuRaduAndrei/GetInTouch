using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.DataAccess.Infrastructure
{
    public interface INotificationRepository : IBaseRepository<NotificationModel>
    {
        IEnumerable<NotificationModel> GetAllForUser(Guid userId);
        NotificationModel GetFriendshipNotification(Guid senderId, Guid receiverId);
        NotificationModel GetAppreciationNotification(Guid postId);
        NotificationModel GetById(Guid notificationId);
        IEnumerable<NotificationModel> GetNotificationsForPost(Guid postId);
    }
}
