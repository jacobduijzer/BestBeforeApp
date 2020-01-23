using BestBeforeApp.Shared;
using Microsoft.AppCenter.Analytics;
using Microsoft.Extensions.DependencyInjection;

using Xamarin.Forms;

namespace BestBeforeApp.Products.ProductDetails
{
    [QueryProperty("ProductId", "id")]
    public partial class ProductDetailsPage : BasePage
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
            
            if (!string.IsNullOrEmpty(ProductId))
                ((ProductDetailsViewModel)BindingContext).LoadDetailsCommand.Execute(int.Parse(ProductId));
        }
    }
}
