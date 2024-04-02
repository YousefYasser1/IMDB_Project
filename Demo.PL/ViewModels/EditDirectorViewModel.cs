﻿using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class EditDirectorViewModel
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(10)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(10)]
        public string LastName { get; set; }

        [Range(25, 70)]
        public int Age { get; set; }

        public string ImageName { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
