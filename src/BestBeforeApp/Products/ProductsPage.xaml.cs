using System;
using BestBeforeApp.Shared;
using Microsoft.AppCenter.Analytics;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace BestBeforeApp.Products
{
    public partial class ProductsPage : ContentPage, ISearchPage
    {
        //readonly WeakEventManager<string> _searchTextChangedEventManager = new WeakEventManager<string>();
        private readonly ProductsViewModel _productsViewModel;

        public ProductsPage()
        {
            InitializeComponent();

            BindingContext = _productsViewModel = App.ServiceProvider.GetService<ProductsViewModel>();

            
            //settingsToolbarItem.Clicked += HandleSettingsToolbarItem;
            //ToolbarItems.Add(settingsToolbarItem);
        }

        public event EventHandler<string> SearchBarTextChanged
        {
            add => Console.WriteLine("Add");
            remove => Console.WriteLine("Remove");
        }

        public void OnSearchBarTextChanged(in string text) => Console.WriteLine("Handle"); //_searchTextChangedEventManager.HandleEvent(this, text, nameof(SearchBarTextChanged));

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Analytics.TrackEvent(nameof(ProductsPage));

            if (_productsViewModel.Products == null || _productsViewModel.Products.Count == 0)
                _productsViewModel.LoadItemsCommand.Execute(null);
        }

        private void HandleSearchBarTextChanged(object sender, string searchBarText) => Console.WriteLine("DoCmd"); //ViewModel.FilterRepositoriesCommand.Execute(searchBarText);
    }
}
