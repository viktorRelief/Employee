using System.ComponentModel.DataAnnotations;

namespace EmployeesProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string EmployeeLogin { get; set; }
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"[(][0-9]{3}[)] [0-9]{3}-[0-9]{4}", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string HomeAddress { get; set; }
        public int DepartmentId { get; set; }      
        public Department Department { get; set; }
    }
}
