using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetInTouch.DataAccess.Implementation
{
    public class PostRepository : BaseRepository<PostModel>, IPostRepository
    {
        public PostRepository(GetInTouchDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<PostModel> Get()
        {
            return _dbContext.Posts
                .Include(p => p.Comnents)
                .ThenInclude(p => p.Sender)
                .OrderByDescending(p => p.UploadDate);
        }

        public PostModel Get(Guid postId)
        {
            return _dbContext.Posts.Include(p => p.Appreciations)
                .Include(p => p.User)
                .Include(p => p.Comnents)
                .FirstOrDefault(p => p.Id == postId);
        }

        public IEnumerable<PostModel> GetAllForUser(Guid userId)
        {
            return _dbContext.Posts
                .Where(p => p.UserId == userId)
                .Include(p => p.Comnents)
                .ThenInclude(p => p.Sender)
                .OrderByDescending(p => p.UploadDate);
        }
    }
}
