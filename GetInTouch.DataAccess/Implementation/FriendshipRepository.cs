using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetInTouch.DataAccess.Implementation
{
    public class FriendshipRepository : BaseRepository<FriendshipModel>, IFriendshipRepository
    {
        public FriendshipRepository(GetInTouchDbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckFriendship(Guid userId1, Guid userId2)
        {
            var friendship = _dbContext.Friendships.FirstOrDefault(f => (f.ReceiverId == userId1 && f.SenderId == userId2) ||
                                                                        (f.ReceiverId == userId2 && f.SenderId == userId1));

            return friendship != null;
        }

        public IEnumerable<FriendshipModel> GetAllForUser(Guid userId)
        {
            return _dbContext.Friendships.Where(f => f.SenderId == userId || f.ReceiverId == userId)
                .Include(f => f.Sender)
                .Include(f => f.Receiver);
        }

        public FriendshipModel Get(Guid userId1, Guid userId2)
        {
            return _dbContext.Friendships.Include(f => f.Sender).Include(f => f.Receiver)
                                         .FirstOrDefault(f => (f.ReceiverId == userId1 && f.SenderId == userId2) ||
                                                              (f.ReceiverId == userId2 && f.SenderId == userId1));
        }
    }
}
