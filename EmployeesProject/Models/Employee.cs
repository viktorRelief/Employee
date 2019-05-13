using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string EmployeeLogin { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string HomeAddress { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }      
        public Department Department { get; set; }
    }
}
