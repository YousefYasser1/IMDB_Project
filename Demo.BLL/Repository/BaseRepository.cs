using Demo.BLL.InterFace;
using Demo.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
            => _context.Set<T>().Add(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        public void Edit(T entity)
            => _context.Set<T>().Update(entity);

        public async Task<List<T>> GetAll()
            => await _context.Set<T>().ToListAsync();

        public async Task<T> GetById(int id)
            => await _context.Set<T>().FindAsync(id);

    }
}
