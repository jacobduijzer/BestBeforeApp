using BestBeforeApp.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace BestBeforeApp.Products
{
    public partial class ProductsPage : BasePage
    {
        public ProductsPage()
        {
            InitializeComponent();
            base.BindingContext = App.ServiceProvider.GetService<ProductsViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ProductsList.SelectedItem = null;
            ((ProductsViewModel)BindingContext).LoadItemsCommand.Execute(null);
        }
    }
}
