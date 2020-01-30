using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BestBeforeApp.Products.Specifications;
using MediatR;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace BestBeforeApp.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;

        public ICommand AddProductCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand LoadItemsCommand { get; }
        public ICommand ShowDetailsCommand { get; }
        //public ICommand SearchCommand { get; }

        //public ProductsViewModel(IMediator mediator)
        public ProductsViewModel(IMediator mediator)
        {
            AddProductCommand = new AsyncCommand(NavigateToAddProductPageAsync);
            SettingsCommand = new AsyncCommand(NavigateToSettingsAsync);
            LoadItemsCommand = new AsyncCommand(GetProductsAsync);
            ShowDetailsCommand = new AsyncCommand<Product>(ShowDetailsAsync);
            //SearchCommand = new AsyncCommand<string>(SearchAsync);

            _mediator = mediator;
        }

        private async Task NavigateToSettingsAsync() =>
            await Shell.Current.GoToAsync("settings").ConfigureAwait(false);

        private async Task NavigateToAddProductPageAsync() =>
            await Shell.Current.GoToAsync("addproduct").ConfigureAwait(false);

        private string _searchString;
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private IList<Product> _products;
        public IList<Product> Products =>
            string.IsNullOrEmpty(SearchString) ?
                _products : _products.Where(x => x.Name.ToLower().Contains(SearchString.ToLower())).ToList();

        private async Task GetProductsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Analytics.TrackEvent($"{this.GetType().Name} - GetProductsAsync");
                var products = await _mediator
                       .Send(new RetrieveProducts(new AllProductsSpecification()))
                       .ConfigureAwait(false);

                _products = new List<Product>(products);
                OnPropertyChanged(nameof(Products));
            }
            catch (Exception ex)
            {
                var exeptionParam = new Dictionary<string, string>
                {
                    { "GetProductsAsync", ex.Message }
                };
                Crashes.TrackError(ex, exeptionParam);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ShowDetailsAsync(Product product)
        {
            if (product != null)
                await Shell.Current.GoToAsync($"productdetails?id={product.Id}");
        }
    }
}
