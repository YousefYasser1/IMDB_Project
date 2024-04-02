using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites
{
    public class Genre
    {
        public int ID { get; set; }


        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual List<Movie>? Movies { get; set; }
    }
}
