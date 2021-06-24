using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Model
{
    public class PostReactionInfoModel
    {
        public Emoji Emoji { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public bool IsFriend { get; set; }
        public string Image { get; set; }
    }
}
