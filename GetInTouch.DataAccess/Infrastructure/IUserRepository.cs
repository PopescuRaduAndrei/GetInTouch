using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.DataAccess.Infrastructure
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        UserModel GetUserFromIdentity(string userId);
        UserModel Get(Guid senderId);
    }
}
