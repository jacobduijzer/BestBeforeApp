using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using BestBeforeApp.Products.Handlers;
using BestBeforeApp.Shared;
using MediatR;
using Microsoft.AppCenter.Analytics;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace BestBeforeApp.Products.ProductDetails
{
    public class ProductDetailsViewModel
        : BaseViewModel
    {
        public Product Product { get; private set; }
        public ImageSource ProductPhoto { get; private set; }
        public ICommand LoadDetailsCommand { get; }
        public ICommand DeleteProductCommand { get; }

        private readonly IMediator _mediator;
        private readonly ITranslator _translator;

        public bool HasPhoto { get; private set; }

        public ProductDetailsViewModel(
            IMediator mediator,
            ITranslator translator)
        {
            _mediator = mediator;
            _translator = translator;
            LoadDetailsCommand = new AsyncCommand<int>(LoadProductDetailsAsync);
            DeleteProductCommand = new AsyncCommand(DeleteProductAsync);
        }

        private async Task LoadProductDetailsAsync(int productId)
        {
            Product = await _mediator.Send(new GetProductDetails(productId)).ConfigureAwait(false);
            HasPhoto = false;
            if (Product != null && Product.Photo != null)
            {
                ProductPhoto = ImageSource.FromStream(() => new MemoryStream(Product.Photo));
                OnPropertyChanged(nameof(ProductPhoto));

                HasPhoto = true;
            }

            OnPropertyChanged(nameof(Product));
            OnPropertyChanged(nameof(HasPhoto));
        }

        private async Task DeleteProductAsync()
        {
            Analytics.TrackEvent($"{this.GetType().Name} - DeleteProductAsync");

            var result = await Shell.Current.DisplayActionSheet(_translator.Translate("DeleteQuestion"), _translator.Translate("DeleteQuestionNoAnswer"), _translator.Translate("DeleteQuestionYesAnswer"));
            if(result.Equals(_translator.Translate("DeleteQuestionYesAnswer")))
            {
                Analytics.TrackEvent($"{this.GetType().Name} - DeleteProductAsync - Deleting");
                await _mediator.Publish(new DeleteProduct(Product)).ConfigureAwait(false);
                await Shell.Current.Navigation.PopAsync().ConfigureAwait(false);
            }                
        }
    }
}
