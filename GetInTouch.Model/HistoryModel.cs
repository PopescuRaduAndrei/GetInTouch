using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Model
{
    public enum HistoryActionTypes { AppreciatedPost, CommentedOnPost, VisitedProfile}
    public class HistoryModel
    {
        public Guid Id { get; set; }
        public PostModel Post { get; set; }
        public Guid? PostId { get; set; }
        public UserModel UserProfile { get; set; }
        public Guid? UserProfileId { get; set; }
        public HistoryActionTypes Type { get; set; }
        public string Message { get; set; }
        public UserModel User { get; set; }
        public Guid? UserId { get; set; }
        public int NumberOfVisitedProfile { get; set; }
        public DateTime CreatedOn { get; set; }
        public string StringCreatedOn { get; set; }
    }
}
