using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class DirectorViewModel
    {
        [Required]
        [MaxLength(10)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(10)]
        public string LastName { get; set; }

        [Range(25, 70)]
        public int Age { get; set; }
        public IFormFile? ImageFile{ get; set; }
    }
}
