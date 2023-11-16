using Blog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        IEnumerable<T> GetAll(int? page, int? pageSize);
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
        int GetTotalCount();
    }
}
