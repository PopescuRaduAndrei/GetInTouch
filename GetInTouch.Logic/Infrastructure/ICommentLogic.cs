using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.Infrastructure
{
    public interface ICommentLogic
    {
        CommentModel CreateOrUpdateComment(Guid postId, Guid userId, string commentMessage);
        int GetPostNumberComments(Guid postId);
    }
}
