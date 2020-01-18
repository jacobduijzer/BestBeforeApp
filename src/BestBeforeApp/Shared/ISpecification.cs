using System;
using System.Linq.Expressions;

namespace BestBeforeApp.Shared
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Expression { get; }
    }
}
