using System.Collections.Generic;

namespace EmployeesProject.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Employee> Employee { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
