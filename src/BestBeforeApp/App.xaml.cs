using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BestBeforeApp.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Plugin.Multilingual;
using BestBeforeApp.Resources;

namespace BestBeforeApp
{
    public partial class App : Application
    {
        private const string APPCENTERIOS = "c002c4dc-90cd-4b9e-9c62-37f3f9d872a3";
        private const string APPCENTERANDROID = "a530f0ea-d70b-43ac-aa17-ec7a98394c5e";

        public static IServiceProvider ServiceProvider { get; set; }
        
        public App()
        {
            InitializeComponent();

            var culture = CrossMultilingual.Current.DeviceCultureInfo;
            AppResources.Culture = culture;

            DependencyService.Register<MockDataStore>();
            MainPage = ServiceProvider.GetService<AppShell>();
        }

        protected override void OnStart()
        {
//#if !DEBUG
            AppCenter.Start($"ios={APPCENTERIOS};" +
                $"android={APPCENTERANDROID};", 
                typeof(Analytics), 
                typeof(Crashes),
                typeof(Distribute));
//#endif
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
