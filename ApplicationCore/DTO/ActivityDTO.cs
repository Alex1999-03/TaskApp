using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO
{
    public class ActivityDTO 
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; } = null!;
        public bool IsDone { get; private set; }
        public int TaskListId { get; set; }
    }
}
