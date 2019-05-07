using System.ComponentModel.DataAnnotations;

namespace EmployeesProject.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(10)]
        public string EmployeeLogin { get; set; }
        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(10)]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string HomeAddress { get; set; }
        [Required]
        public string Department { get; set; }
    }
}
