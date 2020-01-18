using System.Threading;
using System.Threading.Tasks;
using BestBeforeApp.Shared;
using MediatR;

namespace BestBeforeApp.Products.Handlers
{
    public class DeleteProductHandler : INotificationHandler<DeleteProduct>
    {
        private readonly IRepository<Product> _productRepository;

        public DeleteProductHandler(IRepository<Product> productRepository) => _productRepository = productRepository;

        public async Task Handle(DeleteProduct notification, CancellationToken cancellationToken) =>
            await _productRepository.Delete(notification.Product).ConfigureAwait(false);
    }
}
