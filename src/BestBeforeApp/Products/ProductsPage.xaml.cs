using Microsoft.AppCenter.Analytics;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace BestBeforeApp.Products
{
    public partial class ProductsPage : ContentPage
    {
        private readonly ProductsViewModel _productsViewModel;

        public ProductsPage()
        {
            InitializeComponent();

            BindingContext = _productsViewModel = App.ServiceProvider.GetService<ProductsViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Analytics.TrackEvent(nameof(ProductsPage));

            if (_productsViewModel.Products.Count == 0)
                _productsViewModel.LoadItemsCommand.Execute(null);
        }
    }
}
