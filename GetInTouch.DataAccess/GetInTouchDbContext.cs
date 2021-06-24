using GetInTouch.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace GetInTouch.DataAccess
{
    public class GetInTouchDbContext : DbContext
    {
        public GetInTouchDbContext(DbContextOptions<GetInTouchDbContext> options) : base(options)
        { }

        public DbSet<PostModel> Posts { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<AppreciationModel> Appreciations { get; set; }
        public DbSet<FriendshipModel> Friendships { get; set; }
        public DbSet<NotificationModel> Notifications { get; set; }
        public DbSet<HistoryModel> Histories { get; set; }
    }
}