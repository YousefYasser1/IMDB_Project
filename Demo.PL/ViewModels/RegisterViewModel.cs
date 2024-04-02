using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Enter Valid Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password is not Match")]
        [DataType(DataType.Password)]
        public string ConfirmePassword { get; set; }


        [Range(maximum:35, minimum:18, ErrorMessage = "Age Must be between 18 & 35")]
        [Required]
        public int Age { get; set; }


        [Required]
        public string Address { get; set; }

        public IFormFile ImageProfile { get; set; }
    }
}
