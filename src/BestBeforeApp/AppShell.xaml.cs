using System;
using System.Collections.Generic;
using BestBeforeApp.Products.AddProduct;
using BestBeforeApp.Products.ProductDetails;
using BestBeforeApp.Settings;
using Xamarin.Forms;

namespace BestBeforeApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public Dictionary<string, Type> Routes { get; } = new Dictionary<string, Type>();

        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routes.Add("productdetails", typeof(ProductDetailsPage));
            Routes.Add("addproduct", typeof(AddProductPage));
            Routes.Add("settings", typeof(SettingsPage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}
