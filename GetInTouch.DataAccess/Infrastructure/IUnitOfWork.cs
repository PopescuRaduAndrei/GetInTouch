using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.DataAccess.Infrastructure
{
    public interface IUnitOfWork
    {
        void Save();
    }
}
