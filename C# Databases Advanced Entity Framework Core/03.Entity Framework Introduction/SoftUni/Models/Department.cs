﻿using System;
using System.Collections.Generic;

namespace SoftUni.Models
{
    public partial class Department
    {
        public Department()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }

        public  Employee Manager { get; set; }
        public  ICollection<Employee> Employees { get; set; }
    }
}
