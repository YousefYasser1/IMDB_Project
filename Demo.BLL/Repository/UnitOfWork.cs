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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<Genre> Genre { get; private set; }
        public IBaseRepository<Director> Director { get; private set; }
        public IMovieRepository Movie { get; private set; }
        public IBaseRepository<Actor> Actor { get; private set; }
        public IMovieCommentRepository MovieComment{ get; private set; }
        public IMovieActorRepository MoviesActors { get; private set; }
        public ILikeMovieRepository LikeMovie{ get; private set; }
        public IMovieUserRepository MovieUser { get; private set;    }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Genre = new BaseRepository<Genre>(_context);
            Director = new BaseRepository<Director>(_context);
            Movie = new MovieRepository(_context);
            LikeMovie = new LikeMovieBaseRepository(_context);
            Actor = new BaseRepository<Actor>(_context);
            MovieComment = new MovieCommentRepository(_context);
            MoviesActors = new MovieActorRepository (_context);
            MovieUser = new MovieUserRepository(_context);
        }

        public async Task<int> Complete()
            => await _context.SaveChangesAsync();

        public void Dispose()
        {
           _context.Dispose();  
        }
    }
}
