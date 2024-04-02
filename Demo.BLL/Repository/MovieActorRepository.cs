using Demo.BLL.InterFace;
using Demo.DAL.Context;
using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repository
{
    public class MovieActorRepository : BaseRepository<MovieActor>, IMovieActorRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieActorRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public List<MovieActor> GetMovieActorByMovieID(int movieID)
        {
            var movieActor = _context.MoviesActors.Where(x => x.MovieID == movieID).ToList();
            return movieActor;
        }

        public MovieActor GetMovieActorByMovieIdActorId(int movieID, int actorID)
        {
            var movieActor = _context.MoviesActors.FirstOrDefault(x => x.MovieID == movieID && x.ActorID == actorID);
            return movieActor;
        }
    }
}
