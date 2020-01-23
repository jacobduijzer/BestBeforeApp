using System.Threading;
using System.Threading.Tasks;
using BestBeforeApp.Products;
using BestBeforeApp.Shared;
using MediatR;

namespace BestBeforeApp.Products.ProductDetails
{
    public class GetProductDetailsHandler : IRequestHandler<GetProductDetails, Product>
    {
        private readonly IRepository<Product> _productRepository;

        public GetProductDetailsHandler(IRepository<Product> productRepository) =>
            _productRepository = productRepository;

        public async Task<Product> Handle(GetProductDetails request, CancellationToken cancellationToken) =>
            await _productRepository.GetById(request.ProductId).ConfigureAwait(false);
    }
}
