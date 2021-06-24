using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Model
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public UserModel Sender { get; set; }
        public Guid? SenderId { get; set; }
        public PostModel Post { get; set; }
        public Guid PostId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
