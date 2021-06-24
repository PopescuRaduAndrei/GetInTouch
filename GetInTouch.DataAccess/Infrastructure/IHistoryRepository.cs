using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.DataAccess.Infrastructure
{
    public interface IHistoryRepository : IBaseRepository<HistoryModel>
    {
        IEnumerable<HistoryModel> GetAllForUser(Guid userId);
        HistoryModel GetPostHistory(Guid userId, Guid postId, HistoryActionTypes type);
        HistoryModel GetVisitedProfileHistory(Guid userId, Guid userProfileId);
    }
}
