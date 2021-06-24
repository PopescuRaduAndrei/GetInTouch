using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.DataAccess.Implementation
{
    public class CommentRepository : BaseRepository<CommentModel>, ICommentRepository
    {
        public CommentRepository(GetInTouchDbContext dbContext) : base(dbContext)
        {
        }
    }
}
