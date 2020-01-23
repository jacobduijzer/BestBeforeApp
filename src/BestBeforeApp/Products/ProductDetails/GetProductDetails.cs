using BestBeforeApp.Products;
using MediatR;

namespace BestBeforeApp.Products.ProductDetails
{
    public class GetProductDetails : IRequest<Product>
    {
        public GetProductDetails(int productId) => ProductId = productId;

        public int ProductId { get; }
    }
}
