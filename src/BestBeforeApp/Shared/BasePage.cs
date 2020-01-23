using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace BestBeforeApp.Shared
{
    public abstract class BasePage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Analytics.TrackEvent($"{this.GetType().Name} - OnAppearing");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Analytics.TrackEvent($"{this.GetType().Name} - OnDisappearing");
        }
    }
}
