using MediatR;

namespace BestBeforeApp.Products.Handlers
{
    public class DeleteProduct : INotification
    {
        public DeleteProduct(Product product) => Product = product;

        public Product Product { get; }
    }
}
