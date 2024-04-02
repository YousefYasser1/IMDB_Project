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
    public class MovieRepository: BaseRepository<Movie> , IMovieRepository
    {
        private readonly ApplicationDbContext context;

        public MovieRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

        public List<Movie> GetListByName(string name)
        {
            var items = context.Movies.Where(x => x.Name == name).ToList();
            return items;
        }

        public List<int> GetMovieIDs()
        {
            var items = context.Movies.Select(x => x.ID).ToList();
            return items;
        }
    }
}
