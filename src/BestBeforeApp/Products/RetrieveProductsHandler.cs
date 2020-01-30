using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BestBeforeApp.Shared;
using MediatR;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace BestBeforeApp.Products
{
    public class RetrieveProductsHandler
        : IRequestHandler<RetrieveProducts, IEnumerable<Product>>
    {
        private readonly IRepository<Product> _productRepository;

        public RetrieveProductsHandler(IRepository<Product> productRepository) => _productRepository = productRepository;

        public async Task<IEnumerable<Product>> Handle(RetrieveProducts request, CancellationToken cancellationToken)
        {
            Analytics.TrackEvent($"{this.GetType().Name} - GetProductsAsync");
            try
            {
                return await _productRepository.Get(request.Specification.Expression).ConfigureAwait(false);
            }
            catch (System.Exception ex)
            {
                var exeptionParam = new Dictionary<string, string>
                {
                    { "GetProductsAsync", ex.Message }
                };
                Crashes.TrackError(ex, exeptionParam);

                throw;
            }
        }
            
    }
}
