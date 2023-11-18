using Blog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Expression<Func<T, object>>[] includeProperties = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? page = null,
        int? pageSize = null
    );
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
        int GetTotalCount(Expression<Func<T, bool>> filter = null);
    }
}
