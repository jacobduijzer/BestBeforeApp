using Microsoft.AppCenter.Analytics;
using Microsoft.Extensions.DependencyInjection;

using Xamarin.Forms;

namespace BestBeforeApp.ProductDetails
{
    //[QueryProperty("ProductId", "productid")]
    public partial class ProductDetailsPage : ContentPage
    {
        //public int ProductId;
        public ProductDetailsPage()
        {
            InitializeComponent();

            BindingContext = App.ServiceProvider.GetService<ProductDetailsViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Analytics.TrackEvent(nameof(ProductDetailsPage));
        }
    }
}
