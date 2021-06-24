using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetInTouch.DataAccess.Implementation
{
    public class NotificationRepository : BaseRepository<NotificationModel>, INotificationRepository
    {
        public NotificationRepository(GetInTouchDbContext dbContext) : base(dbContext)
        {
        }

        public NotificationModel GetFriendshipNotification(Guid senderId, Guid receiverId)
        {
            return _dbContext.Notifications.FirstOrDefault(n => n.SenderId == senderId &&
                                                                n.ReceiverId == receiverId &&
                                                                n.Type == NotificationTypes.Friendship);
        }

        public IEnumerable<NotificationModel> GetAllForUser(Guid userId)
        {
            return _dbContext.Notifications.Where(n => n.ReceiverId == userId)
                .Include(n => n.Sender)
                .Include(n => n.Receiver)
                .Include(n => n.Post);
        }

        public NotificationModel GetAppreciationNotification(Guid postId)
        {
            return _dbContext.Notifications.FirstOrDefault(n => n.PostId == postId);
        }

        public NotificationModel GetById(Guid notificationId)
        {
            return _dbContext.Notifications.FirstOrDefault(n => n.Id == notificationId);
        }

        public IEnumerable<NotificationModel> GetNotificationsForPost(Guid postId)
        {
            return _dbContext.Notifications.Where(n => n.PostId == postId);
        }
    }
}
