using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Model
{
    public enum NotificationTypes { Friendship, Appreciation }
    public class NotificationModel
    {
        public Guid Id { get; set; }
        public UserModel Sender { get; set; }
        public Guid? SenderId { get; set; }
        public UserModel Receiver { get; set; }
        public Guid? ReceiverId { get; set; }
        public PostModel Post { get; set; }
        public Guid? PostId { get; set; }
        public string Message { get; set; }
        public NotificationTypes Type { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreateOnString { get; set; }
    }
}
