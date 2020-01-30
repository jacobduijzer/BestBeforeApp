using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using BestBeforeApp.Resources;
using System.Globalization;
using Plugin.LocalNotification;
using System.Collections.Generic;
using BestBeforeApp.Products;
using BestBeforeApp.Shared;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

            NotificationCenter.Current.NotificationTapped += LoadPageFromNotification;

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


        private void LoadPageFromNotification(NotificationTappedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data))
            {
                return;
            }

            var serializer = new ObjectSerializer<List<string>>();
            var list = serializer.DeserializeObject(e.Data);
            //if (list.Count != 2)
            //{
            //    return;
            //}
            //if (list[0] != typeof(NotificationPage).FullName)
            //{
            //    return;
            //}
            //var tapCount = list[1];

            //((NavigationPage)MainPage).Navigation.PushAsync(new NotificationPage(int.Parse(tapCount)));
        }
    }
}
