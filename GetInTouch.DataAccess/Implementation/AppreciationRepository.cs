using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetInTouch.DataAccess.Implementation
{
    public class AppreciationRepository : BaseRepository<AppreciationModel>, IAppreciationRepository
    {
        public AppreciationRepository(GetInTouchDbContext dbContext) : base(dbContext)
        {
        }

        public AppreciationModel Get(Guid postId, Guid userId)
        {
            return _dbContext.Appreciations.FirstOrDefault(a => a.PostId == postId && a.SenderId == userId);
        }

        public IEnumerable<AppreciationModel> GetAppreciationsForPost(Guid postId)
        {
            return _dbContext.Appreciations.Where(a => a.PostId == postId).Include(a => a.Sender);
        }
    }
}
