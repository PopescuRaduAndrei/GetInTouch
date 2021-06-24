using GetInTouch.Logic.ViewModels.Appreciation;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.Post
{
    public class PostsViewModel
    {
        public IEnumerable<DetailsPostViewModel> Posts { get; set; }
        public IEnumerable<FriendshipModel> Friendships { get; set; }
        public UserModel ActiveUser { get; set; }
    }
}
