using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class EditMovieViewModel
    {

        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Poster is Required...")]
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }


        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Video is Required...")]
        public IFormFile VideoFile { get; set; }
        public string VideoName { get; set; }   
        public int GenreID { get; set; }
        public int DirectorID { get; set; }
    }
}
