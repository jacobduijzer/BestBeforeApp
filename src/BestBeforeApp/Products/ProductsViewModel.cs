using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MediatR;
using Microsoft.AppCenter.Crashes;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace BestBeforeApp.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;

        public ObservableCollection<Product> Products { get; private set; }

        public ICommand LoadItemsCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand ShowDetailsCommand { get; }

        public ProductsViewModel(IMediator mediator)
        {
            Products = new ObservableCollection<Product>();

            LoadItemsCommand = new AsyncCommand(GetProductsAsync);
            //DeleteProductCommand = new AsyncCommand<Product>(DeleteProductAsync);
            ShowDetailsCommand = new AsyncCommand<Product>(ShowDetailsAsync);

            _mediator = mediator;
        }

        private async Task GetProductsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Products.Clear();

                var products = await _mediator
                    .Send(new RetrieveProducts(new AllProductsSpecification()))
                    .ConfigureAwait(false);

                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteProductAsync(Product product)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //await _mediator.
                await GetProductsAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ShowDetailsAsync(Product product)
        {
            //await Shell.Current.GoToAsync($"//details", true);
        }
    }
}
