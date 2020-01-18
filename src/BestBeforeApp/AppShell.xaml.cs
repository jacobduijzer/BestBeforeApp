using System;
using System.Collections.Generic;
using BestBeforeApp.ProductDetails;
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

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}
