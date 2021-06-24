using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.Notifications
{
    public class NotificationInfoViewModel
    {
        public Guid NotificationId { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string UserFullName { get; set; }
        public string Message { get; set; }
        public bool ShouldDisplayButtons { get; set; }
        public bool IsCompleted { get; set; }
        public string CreatedOn { get; set; }
    }
}
