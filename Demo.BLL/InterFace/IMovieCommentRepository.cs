using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.InterFace
{
    public interface IMovieCommentRepository: IBaseRepository<MovieComment>
    {
        List<MovieComment> GetCommentOfMovie(int movieId);
    }
}
