

using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services.Repos
{
   public class BaseRepo<T> : IBase<T> where T : class
    {
        private AppDBContext _db;
        public BaseRepo(AppDBContext db)
        {
            _db = db;
        }

        public async Task<T> find(Expression<Func<T, bool>> match, string [] Includes=null)
        {
            IQueryable<T> query = _db.Set<T>();
            if (Includes != null)
                foreach (var item in Includes)
                    query = query.Include(item);
            return  await query.SingleOrDefaultAsync(match);
        }
        public IEnumerable<T> findAll(Expression<Func<T, bool>> match, string[] Includes = null)
        {
            IQueryable<T> query = _db.Set<T>();
            if (Includes != null)
                foreach (var item in Includes)
                    query = query.Include(item);
            return query.Where(match).ToList();
        }

        public async Task<IEnumerable<T>> getAll()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T> GetByID(int Id)
        {
            return await _db.Set<T>().FindAsync(Id);
        }


        public async Task<T> AddOne(T obj)
        {
            await _db.Set<T>().AddAsync(obj);
            return obj;
        }
        public T Update(T entity)
        {
            _db.Update(entity);
            return entity;
        }
        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }
        public T Attach(T entity)
        {
            _db.Set<T>().Attach(entity);
            return entity;
        }

        public int Count(Expression<Func<T, bool>> match)
        {
            return _db.Set<T>().Count(match);
        }
        //T Update(T entity);
        //void Delete(T entity);
        //T Attach(T entity);
        //int Count(Expression<Func<T, bool>> match);
    }
}
