using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.Friendship
{
    public class FriendsViewModel
    {
        public Guid ProfileUserId { get; set; }
        public UserModel ActiveUser { get; set; }
        public IEnumerable<FriendViewModel> Friends { get; set; }
    }
}
