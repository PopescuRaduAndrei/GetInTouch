using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Model
{
    public enum Emoji { None, Like, Love, Haha, Wow, Angry, Sad };
    public enum RestrictViewingType { OnlyMe, OnlyCloseFriends, Everyone};

    public class PostModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public UserModel User { get; set; }
        public Guid UserId { get; set; }
        public int AppreciationsNumber { get; set; }
        public int CommentsNumber { get; set; }
        public int SHaresNumber { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadTime { get; set; }
        public bool NotificationsOn { get; set; }
        public RestrictViewingType RestrictViewingType { get; set; }
        public List<AppreciationModel> Appreciations { get; set; }
        public List<CommentModel> Comnents { get; set; }
    }
}
