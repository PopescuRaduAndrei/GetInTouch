using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetInTouch.DataAccess.Implementation
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        public UserRepository(GetInTouchDbContext dbContext) : base(dbContext)
        {
        }

        public UserModel Get(Guid senderId)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == senderId);
        }

        public UserModel GetUserFromIdentity(string userId)
        {
            return _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
        }
    }
}
