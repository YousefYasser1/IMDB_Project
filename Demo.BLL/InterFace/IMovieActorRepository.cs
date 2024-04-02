using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.InterFace
{
    public interface IMovieActorRepository: IBaseRepository<MovieActor>
    {
        List<MovieActor> GetMovieActorByMovieID(int movieID);
        MovieActor GetMovieActorByMovieIdActorId(int  movieID , int actorID);
    }
}
