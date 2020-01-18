using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using BestBeforeApp.Resources;
using System.Globalization;

namespace BestBeforeApp
{
    public partial class App : Application
    {
        private const string APPCENTERIOS = "c002c4dc-90cd-4b9e-9c62-37f3f9d872a3";
        private const string APPCENTERANDROID = "a530f0ea-d70b-43ac-aa17-ec7a98394c5e";

        public static IServiceProvider ServiceProvider { get; set; }
        public static CultureInfo AppCulture { get; private set; }

        public App()
        {
            InitializeComponent();

            AppCulture = new CultureInfo("nl-NL");
            CultureInfo.CurrentCulture = AppCulture;
            AppResources.Culture = AppCulture;

            MainPage = ServiceProvider.GetService<AppShell>();
        }

        protected override void OnStart() =>
            AppCenter.Start($"ios={APPCENTERIOS};android={APPCENTERANDROID};",
                typeof(Analytics),
                typeof(Crashes),
                typeof(Distribute));

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
