using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITaskListRepository TasksLists { get; }
        IActivityRepository Activities { get; }
        Task<int> SaveChangesAsync();
    }
}
