using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace BestBeforeApp.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        private readonly IProductsService _productsService;

        public ObservableCollection<Product> Products { get; private set; }

        public ICommand LoadItemsCommand { get; }

        public ProductsViewModel(IProductsService productsService)
        {
            _productsService = productsService;

            Products = new ObservableCollection<Product>();

            LoadItemsCommand = new AsyncCommand(GetProductsAsync);
        }

        private async Task GetProductsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Products.Clear();
                var products = await _productsService.GetProducts().ConfigureAwait(false);

                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
