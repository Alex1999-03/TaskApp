using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class TaskList : IBaseEntity
    {
        public TaskList()
        {
            Activities = new HashSet<Activity>();
        }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; } = null!;
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
