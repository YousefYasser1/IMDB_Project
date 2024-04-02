using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class GenreViewModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "MaxLength is 20")]
        public string Name { get; set; }
    }
}
