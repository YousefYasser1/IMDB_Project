using Demo.BLL.InterFace;
using Demo.DAL.Context;
using Demo.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repository
{
    public class MovieUserRepository: BaseRepository<MoviesUser> , IMovieUserRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieUserRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }



        public MoviesUser GetItemByUserIdMovieId(int movieID, int UserID)
        {
            var item = _context.MoviesUsers.FirstOrDefault(x => x.MovieID == movieID && x.UserId == UserID);
            return item;
        }

        public List<int> MoviesIds(int UserId)
        {
            var ids = _context.MoviesUsers.Where(x => x.UserId == UserId).Select(x => x.MovieID).ToList();
            return ids;
        }
    }
}
