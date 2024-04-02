using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites
{
    public class Movie
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string VideoName { get; set; }

        [ForeignKey("Genre")]
        public int GenreID { get; set; }
        public virtual Genre Genre { get; set; }


        [ForeignKey("Director")]
        public int DirectorID { get; set; }
        public virtual Director Director { get; set; }

        public virtual List<LikeMovie> Likes { get; set; }
        public virtual List<MovieComment> MovieComments { get; set; }
        public virtual List<MovieActor> MovieActors { get; set; }
        public virtual List<MoviesUser> MoviesUsers{ get; set; }
    }
}
