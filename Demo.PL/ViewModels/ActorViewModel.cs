using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ActorViewModel
    {
        [Required]
        [MaxLength(10)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(10)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Image Profile is Required")]
        public IFormFile ImageFile { get; set; }
    }
}
