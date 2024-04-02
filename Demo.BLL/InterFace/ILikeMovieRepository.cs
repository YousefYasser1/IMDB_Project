using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.InterFace
{
    public interface ILikeMovieRepository: IBaseRepository<LikeMovie>
    {
        LikeMovie? GetFirstBasedOnCondition(int movieId , int userId);
        int GetNumberLikes(List<LikeMovie> likes , int movieId);
    }
}
