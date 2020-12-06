using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain
{
    interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        T Get(Expression<Func<T, bool>> criteria);

        void Save(T entity);

        Task SaveAsync(T entity);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Delete(T entity);

        Task DeleteAsync(T entity);
    }
}
