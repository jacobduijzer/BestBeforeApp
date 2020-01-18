using Microsoft.AppCenter.Analytics;
using Microsoft.Extensions.DependencyInjection;

using Xamarin.Forms;

namespace BestBeforeApp.ProductDetails
{
    [QueryProperty("ProductId", "id")]
    public partial class ProductDetailsPage : ContentPage
    {
        public string ProductId { set; get; }

        public ProductDetailsPage()
        {
            InitializeComponent();
            BindingContext = App.ServiceProvider.GetService<ProductDetailsViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Analytics.TrackEvent(nameof(ProductDetailsPage));

            if (!string.IsNullOrEmpty(ProductId))
                ((ProductDetailsViewModel)BindingContext).LoadDetailsCommand.Execute(int.Parse(ProductId));
        }
    }
}
