using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.InterFace
{
    public interface IMovieUserRepository: IBaseRepository<MoviesUser>
    {
        MoviesUser GetItemByUserIdMovieId(int movieID, int UserID);
        List<int> MoviesIds(int UserId);
    }
}
