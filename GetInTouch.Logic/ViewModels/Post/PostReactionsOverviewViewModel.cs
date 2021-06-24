using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.Post
{
    public class PostReactionsOverviewViewModel
    {
        public Guid LoggedInUserId { get; set; }
        public Guid PostId { get; set; }
        public IEnumerable<PostReactionInfoModel> PostReactionInfoModels { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfLoves { get; set; }
        public int NumberOfHaha { get; set; }
        public int NumberOfAngry { get; set; }
        public int NumberOfSad { get; set; }
        public int NumberOfWow { get; set; }
    }
}
