using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BestBeforeApp.Services;

namespace BestBeforeApp
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = ServiceProvider.GetService<AppShell>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
