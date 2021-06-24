using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.DataAccess.Infrastructure
{
    public interface IPostRepository : IBaseRepository<PostModel>
    {
        IEnumerable<PostModel> Get();
        PostModel Get(Guid postId);
        IEnumerable<PostModel> GetAllForUser(Guid userId);
    }
}
