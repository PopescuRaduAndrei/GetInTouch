using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.Friendship
{
    public class FriendViewModel
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public bool ShouldDisplayUnfriendButton { get; set; }
        public bool ShouldDisplayAddFriendButton { get; set; }
        public bool ShouldDisplaySeeProfileButton { get; set; }
    }
}
