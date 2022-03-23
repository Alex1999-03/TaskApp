using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskAppContext _context;
        public UnitOfWork(TaskAppContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            TasksLists = new TaskListRepository(_context);
            Activities = new ActivityRepository(_context);
        }

        public IUserRepository Users { get; private set; }

        public ITaskListRepository TasksLists { get; private set; }

        public IActivityRepository Activities { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
