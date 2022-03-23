using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class TaskListRepository : EfRepository<TaskList>, ITaskListRepository
    {
        public TaskListRepository(TaskAppContext context) : base(context)
        {

        }

        public async Task<IEnumerable<TaskList>> GetAllWithActivitiesAsync()
        {
            return await _context.Set<TaskList>().Include(x => x.Activities).ToListAsync();
        }
    }
}
