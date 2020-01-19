using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BestBeforeApp.Products.Specifications;
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

        public ICommand LoadItemsCommand { get; }
        public ICommand ShowDetailsCommand { get; }
        public ICommand SearchCommand { get; }

        public ProductsViewModel(IMediator mediator)
        {
            LoadItemsCommand = new AsyncCommand(GetProductsAsync);
            ShowDetailsCommand = new AsyncCommand<Product>(ShowDetailsAsync);
            SearchCommand = new AsyncCommand<string>(SearchAsync);

            _mediator = mediator;
        }

        private Task SearchAsync(string arg)
        {
            throw new NotImplementedException();
        }

        private async Task GetProductsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var products = await _mediator
                       .Send(new RetrieveProducts(new AllProductsSpecification()))
                       .ConfigureAwait(false);

                _products = new List<Product>(products);
                OnPropertyChanged(nameof(Products));
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

        private async Task ShowDetailsAsync(Product product) =>
            await Shell.Current.GoToAsync($"productdetails?id={product.Id}");
    }
}
