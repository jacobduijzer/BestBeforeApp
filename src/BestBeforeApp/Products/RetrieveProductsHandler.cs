using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BestBeforeApp.Shared;
using MediatR;

namespace BestBeforeApp.Products
{
    public class RetrieveProductsHandler
        : IRequestHandler<RetrieveProducts, IEnumerable<Product>>
    {
        private readonly IRepository<Product> _productRepository;

        public RetrieveProductsHandler(IRepository<Product> productRepository) => _productRepository = productRepository;

        public async Task<IEnumerable<Product>> Handle(RetrieveProducts request, CancellationToken cancellationToken) =>
            await _productRepository.Get(request.Specification.Expression).ConfigureAwait(false);
    }
}
