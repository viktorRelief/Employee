using System.ComponentModel.DataAnnotations;

namespace EmployeesProject.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is incorrect")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is incorrect")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
