using System.ComponentModel.DataAnnotations;

namespace EmployeesProject.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
