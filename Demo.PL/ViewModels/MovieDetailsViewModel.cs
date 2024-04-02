using Demo.DAL.Entites;

namespace Demo.PL.ViewModels
{
    public class MovieDetailsViewModel
    {
        public Movie Movie { get; set; }
        public List<MovieActor> movieActors { get; set; }
    }
}
