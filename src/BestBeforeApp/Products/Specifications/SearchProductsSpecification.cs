using System;
using System.Linq.Expressions;
using BestBeforeApp.Shared;

namespace BestBeforeApp.Products.Specifications
{
    public class SearchProductsSpecification : ISpecification<Product>
    {
        private readonly string _searchString;

        public SearchProductsSpecification(string searchString) =>
            _searchString = searchString;

        public Expression<Func<Product, bool>> Expression => product => product.Name.Contains(_searchString.ToLower());
    }
}
