using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.InterFace
{
    public interface IUnitOfWork: IDisposable
    {
        IBaseRepository<Genre> Genre { get; }
        IBaseRepository<Director> Director { get; }
        IMovieRepository Movie { get; }
        ILikeMovieRepository LikeMovie { get; }
        IBaseRepository<Actor> Actor { get; }
        IMovieActorRepository MoviesActors { get; }
        IMovieCommentRepository MovieComment { get; }
        IMovieUserRepository MovieUser { get; }
        Task<int> Complete();
    }
}
