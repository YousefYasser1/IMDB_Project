using Demo.DAL.Entites;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.PL.ViewModels
{
    public class DisplayMovieViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string VideoName { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Director Director { get; set; }

        public int Likes { get; set; }

        public bool UserHasLike { get; set; }

        public List<MovieComment> Comments { get; set; }
    }
}
