using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class EditProfileViewModel
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Range(18, 35, ErrorMessage = "Age Must be between 18 & 35")]
        public int Age { get; set; }
    }
}
