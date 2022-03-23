﻿using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO
{
    public class TaskListDTO 
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Title { get; set; } = null!;
    }
}
