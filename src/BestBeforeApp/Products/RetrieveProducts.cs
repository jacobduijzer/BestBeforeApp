using System.Collections.Generic;
using BestBeforeApp.Shared;
using MediatR;

namespace BestBeforeApp.Products
{
    public class RetrieveProducts
        : IRequest<IEnumerable<Product>>
    {
        public RetrieveProducts(ISpecification<Product> specification) => Specification = specification;

        public ISpecification<Product> Specification { get; }
    }
}

