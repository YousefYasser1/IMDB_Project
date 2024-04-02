using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class AdminRegsiterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Enter Valid Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password is not Match")]
        public string ConfirmePassword { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
