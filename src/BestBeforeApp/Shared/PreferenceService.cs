using BestBeforeApp.Settings;
using Microsoft.Extensions.Options;
using Xamarin.Essentials;

namespace BestBeforeApp.Shared
{
    public class PreferenceService : IPreferenceService
    {
        private readonly IOptions<AppSettings> _appSettings;
        private const string USE_NOTIFICATIONS = "UseNotifications";
        private const string NUMBER_OF_DAYS_FOR_NOTIFICATION = "NumberOfDaysBeforeExpirationDate";

        public PreferenceService(IOptions<AppSettings> appSettings) => _appSettings = appSettings;

        public bool UseNotifications
        {
            get => Preferences.Get(USE_NOTIFICATIONS, _appSettings.Value.UseNotifications);
            set => Preferences.Set(USE_NOTIFICATIONS, value);
        }

        public int NumberOfDaysBeforeExpirationDate
        {
            get => Preferences.Get(NUMBER_OF_DAYS_FOR_NOTIFICATION, _appSettings.Value.DefaultNumberOfDaysToNotifyBeforeExpire);
            set => Preferences.Set(NUMBER_OF_DAYS_FOR_NOTIFICATION, value);
        }
    }
}