using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.Notifications
{
    public class NotificationsViewModel
    {
        public Guid ActiveUserId { get; set; }
        public IEnumerable<NotificationModel> Notifications { get; set; }
    }
}
