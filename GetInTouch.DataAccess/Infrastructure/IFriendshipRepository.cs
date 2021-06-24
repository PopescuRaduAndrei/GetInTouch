using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.DataAccess.Infrastructure
{
    public interface IFriendshipRepository : IBaseRepository<FriendshipModel>
    {
        IEnumerable<FriendshipModel> GetAllForUser(Guid userId);
        bool CheckFriendship(Guid userId1, Guid userId2);
        FriendshipModel Get(Guid userId1, Guid userId2);
    }
}
