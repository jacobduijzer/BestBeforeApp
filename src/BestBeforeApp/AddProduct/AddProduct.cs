using Ardalis.GuardClauses;
using BestBeforeApp.Products;
using MediatR;

namespace BestBeforeApp.AddProduct
{
    public class AddProduct : INotification
    {
        public AddProduct(Product product)
        {
            Guard.Against.Null(product, nameof(product));
            Product = product;
        }

        public Product Product { get; }
    }
}
