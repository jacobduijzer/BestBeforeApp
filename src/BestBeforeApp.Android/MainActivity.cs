using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BestBeforeApp.Helpers;
using Plugin.CurrentActivity;
using Plugin.LocalNotification;
using Android.Content;

namespace BestBeforeApp.Droid
{
    [Activity(Label = "HoudbaarTot", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            global::Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            NotificationCenter.CreateNotificationChannel(new Plugin.LocalNotification.Platform.Droid.NotificationChannelRequest
            {
                //Sound = Resource.Raw.good_things_happen.ToString()
            });

            LoadApplication(Startup.Init(ConfigureServices));

            NotificationCenter.NotifyNotificationTapped(Intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent)
        {
            NotificationCenter.NotifyNotificationTapped(intent);
            base.OnNewIntent(intent);
        }

        private void ConfigureServices(HostBuilderContext ctx, IServiceCollection services) =>
            services
                .AddSingleton<INativeCalls, NativeCalls>()
                .AddSingleton<IDatabaseFileHelper, DatabaseFileHelper>();

        public class NativeCalls : INativeCalls
        {
            public void OpenToast(string text) =>
                Toast.MakeText(Application.Context, text, ToastLength.Long).Show();
        }
    }
}