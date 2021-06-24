using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetInTouch.DataAccess.Implementation
{
    public class HistoryRepository : BaseRepository<HistoryModel>, IHistoryRepository
    {
        public HistoryRepository(GetInTouchDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<HistoryModel> GetAllForUser(Guid userId)
        {
            return _dbContext.Histories.Where(h => h.UserId == userId).Include(h => h.UserProfile).Include(h => h.Post);
        }

        public HistoryModel GetPostHistory(Guid userId, Guid postId, HistoryActionTypes type)
        {
            return _dbContext.Histories
                .Include(h => h.User)
                .Include(h => h.Post)
                .FirstOrDefault(h => h.UserId == userId && h.PostId == postId && h.Type == type);
        }

        public HistoryModel GetVisitedProfileHistory(Guid userId, Guid userProfileId)
        {
            return _dbContext.Histories
                .Include(h => h.User)
                .Include(h => h.Post)
                .FirstOrDefault(h => h.UserId == userId && h.UserProfileId == userProfileId);
        }
    }
}
