using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace BestBeforeApp.AddProduct
{
    public partial class AddProductPage : ContentPage
    {
        private readonly AddProductViewModel _addProductViewModel;
        public AddProductPage()
        {
            InitializeComponent();

            BindingContext = _addProductViewModel = App.ServiceProvider.GetService<AddProductViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Analytics.TrackEvent(nameof(AddProductPage));
        }
    }
}
