using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBase<T> where T : class
    {
        Task<T> GetByID(int Id);
        Task<IEnumerable<T>> getAll();
        Task<T> AddOne(T obj);
        Task<T> find(Expression<Func<T, bool>> match, string[] Includes = null);
        IEnumerable<T> findAll(Expression<Func<T, bool>> match, string[] Includes = null);
        T Update(T entity);
        void Delete(T entity);
        T Attach(T entity);
        int Count(Expression<Func<T, bool>> match);

    }
}
