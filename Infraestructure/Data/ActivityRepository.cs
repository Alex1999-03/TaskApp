using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class ActivityRepository : EfRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(TaskAppContext context) : base(context)
        {
        }
    }
}
