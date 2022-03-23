using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class Activity : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; } = null!;
        public bool IsDone { get; private set; }
        public int TaskListId { get; set; }

        [JsonIgnore]
        public TaskList TaskList { get; set; } = null!;

        public void ChangeIsDone()
        {
            IsDone = !IsDone;
        }
    }
}
