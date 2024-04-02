using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites
{
    public class Director
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(10)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(10)]
        public string LastName { get; set; }
        [Range(25 , 70)]

        public string FullName { get; set; }
        public int Age { get; set; }
        public string ImageName { get; set; }

        public virtual List<Movie> Movies { get; set; }
    }
}
