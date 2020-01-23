using Ardalis.GuardClauses;
using MediatR;

namespace BestBeforeApp.Products.AddProduct
{
    public class AddProduct : IRequest<int>
    {
        public AddProduct(Product product)
        {
            Guard.Against.Null(product, nameof(product));
            Product = product;
        }

        public Product Product { get; }
    }
}
