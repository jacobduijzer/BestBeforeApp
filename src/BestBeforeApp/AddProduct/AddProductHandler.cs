using System.Threading;
using System.Threading.Tasks;
using BestBeforeApp.Products;
using BestBeforeApp.Shared;
using MediatR;

namespace BestBeforeApp.AddProduct
{
    public class AddProductHandler : INotificationHandler<AddProduct>
    {
        private readonly IRepository<Product> _productRepository;

        public AddProductHandler(IRepository<Product> productRepository) => _productRepository = productRepository;

        public async Task Handle(AddProduct notification, CancellationToken cancellationToken) =>
            await _productRepository.Add(notification.Product).ConfigureAwait(false);
    }
}
