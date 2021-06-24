using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Model
{
    public class FriendshipModel
    {
        public Guid Id { get; set; }
        public UserModel Sender { get; set; }
        public Guid? SenderId { get; set; }
        public UserModel Receiver { get; set; }
        public Guid? ReceiverId { get; set; }
        public DateTime FriendsSince { get; set; }
        public bool FollowsReceiver { get; set; }
        public bool FollowsSender { get; set; }
        public bool HasSenderToCloseFriends { get; set; }
        public bool HasReceiverToCloseFriends { get; set; }
    }
}
