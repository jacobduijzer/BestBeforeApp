using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BestBeforeApp.Shared
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression);

        Task Add(T entity);
    }
}
