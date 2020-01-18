using System;
using System.Linq.Expressions;
using BestBeforeApp.Shared;

namespace BestBeforeApp.Products.Specifications
{
    public class AllProductsSpecification : ISpecification<Product>
    {
        public Expression<Func<Product, bool>> Expression => product => true;
    }
}
