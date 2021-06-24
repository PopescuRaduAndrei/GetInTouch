using GetInTouch.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GetInTouchDbContext _dbContext;

        public UnitOfWork(GetInTouchDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
