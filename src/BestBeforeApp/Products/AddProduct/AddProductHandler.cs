using System.Threading;
using System.Threading.Tasks;
using BestBeforeApp.Shared;
using MediatR;

namespace BestBeforeApp.Products.AddProduct
{
    public class AddProductHandler : IRequestHandler<AddProduct, int>
    {
        private readonly IRepository<Product> _productRepository;

        public AddProductHandler(IRepository<Product> productRepository) => _productRepository = productRepository;

        public async Task<int> Handle(AddProduct notification, CancellationToken cancellationToken) =>
            await _productRepository.Add(notification.Product).ConfigureAwait(false);
    }
}
