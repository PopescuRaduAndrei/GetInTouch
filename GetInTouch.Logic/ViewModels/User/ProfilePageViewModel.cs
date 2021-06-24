using GetInTouch.Logic.ViewModels.Post;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.User
{
    public class ProfilePageViewModel
    {
        public IEnumerable<DetailsPostViewModel> Posts { get; set; }
        public UserModel ActiveUser { get; set; }
        public UserDetailsViewModel UserDetails { get; set; }
        public bool IsMyProfile { get; set; }
        public bool CloseFriends { get; set; }
        public bool IsFollowing { get; set; }
        public bool AreFriends { get; set; }
    }
}
