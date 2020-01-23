using BestBeforeApp.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace BestBeforeApp.Products.AddProduct
{
    public partial class AddProductPage : BasePage
    {
        public AddProductPage()
        {
            InitializeComponent();
            BindingContext = App.ServiceProvider.GetService<AddProductViewModel>();
        }
    }
}
