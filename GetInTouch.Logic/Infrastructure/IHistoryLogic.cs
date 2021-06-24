using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.Infrastructure
{
    public interface IHistoryLogic
    {
        IEnumerable<HistoryModel> GetAllForUser(Guid userId);
        void CreateAppreciationHistory(Guid userId, Guid postId);
        void CreateCommentHistory(Guid userId, Guid postId);
        void CreateVisitedProfileHistory(Guid userId, Guid userProfileId);
    }
}
