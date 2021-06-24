using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.DataAccess.Infrastructure
{
    public interface IAppreciationRepository : IBaseRepository<AppreciationModel>
    {
        AppreciationModel Get(Guid postId, Guid userId);
        IEnumerable<AppreciationModel> GetAppreciationsForPost(Guid postId);
    }
}
