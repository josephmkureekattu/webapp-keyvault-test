﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Employeecs
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        public virtual Department Department { get; set; }
    }
}
