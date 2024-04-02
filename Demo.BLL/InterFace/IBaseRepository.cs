using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.InterFace
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        void Create(T entity);
        void Delete(T entity);
        void Edit(T entity);
        Task<T> GetById(int id);
    }
}
