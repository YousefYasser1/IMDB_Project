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
    public class MovieCommentRepository : BaseRepository<MovieComment>, IMovieCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieCommentRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public List<MovieComment> GetCommentOfMovie(int movieId)
            => _context.MovieComments.Where(x => x.MovieID == movieId).ToList();
    }
}
