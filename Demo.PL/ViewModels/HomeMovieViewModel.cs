using Demo.DAL.Entites;

namespace Demo.PL.ViewModels
{
    public class HomeMovieViewModel
    {
        public List<Movie> Movies { get; set; }
        
        public List<int>MoviesIDs { get; set; }
    }
}
