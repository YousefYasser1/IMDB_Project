using Demo.DAL.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.PL.ViewModels
{
    public class MovieViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Poster is Required...")]
        public IFormFile ImageFile { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Video is Required...")]
        public IFormFile VideoFile { get; set; }
        public int GenreID { get; set; }
        public int DirectorID { get; set; }
    }
}
