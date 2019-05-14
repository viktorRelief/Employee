﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeesProject.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //public List<Employee> Employees { get; set; }
    }
}
