using System.ComponentModel.DataAnnotations;

namespace EmployeesProject.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Email has incorrect format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Incorrect comparison of password")]
        public string ConfirmPassword { get; set; }
    }
}
