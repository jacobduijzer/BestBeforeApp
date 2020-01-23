using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BestBeforeApp.Products;
using BestBeforeApp.Products.Handlers;
using MediatR;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace BestBeforeApp.Products.ProductDetails
{
    public class ProductDetailsViewModel
        : BaseViewModel
    {
        public Product Product { get; private set; }
        public ICommand LoadDetailsCommand { get; }
        public ICommand DeleteProductCommand { get; }

        private readonly IMediator _mediator;

        public ProductDetailsViewModel(IMediator mediator)
        {
            _mediator = mediator;

            LoadDetailsCommand = new AsyncCommand<int>(LoadProductDetailsAsync);
            DeleteProductCommand = new AsyncCommand(DeleteProductAsync);
        }

        private async Task LoadProductDetailsAsync(int productId)
        {
            Product = await _mediator.Send(new GetProductDetails(productId)).ConfigureAwait(false);
            OnPropertyChanged(nameof(Product));
        }

        private async Task DeleteProductAsync()
        {
            await _mediator.Publish(new DeleteProduct(Product)).ConfigureAwait(false);
            await Shell.Current.GoToAsync("///products").ConfigureAwait(false);
        }
    }
}
