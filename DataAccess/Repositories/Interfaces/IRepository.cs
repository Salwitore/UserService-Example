using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        void Update(T entity);

        IQueryable<T> Include(Expression<Func<T, object>> predicate);
    }
}
