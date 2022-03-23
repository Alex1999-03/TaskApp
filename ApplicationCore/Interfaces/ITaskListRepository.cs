using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ITaskListRepository : IGenericRepository<TaskList>
    {
        public Task<IEnumerable<TaskList>> GetAllWithActivitiesAsync();
    }
}
