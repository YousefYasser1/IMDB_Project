using Demo.BLL.InterFace;
using Demo.DAL.Context;
using Demo.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repository
{
    public class LikeMovieBaseRepository:  BaseRepository<LikeMovie>, ILikeMovieRepository
    {
        private readonly ApplicationDbContext _context;
        public LikeMovieBaseRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }
        public LikeMovie? GetFirstBasedOnCondition(int movieId, int userId)
        {
            var item = _context.LikesMovie.SingleOrDefault(x => (x.MovieID == movieId) && (x.UserId == userId));
            return item;
        }

        public int GetNumberLikes(List<LikeMovie> likes, int movieId)
        {
            int count = likes.Count(x => x.MovieID == movieId);
            return count;
        }
    }
}
