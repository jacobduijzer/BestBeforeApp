namespace BestBeforeApp.Shared
{
    public interface IPreferenceService
    {
        bool UseNotifications { get; set; }

        int NumberOfDaysBeforeExpirationDate { get; set; }

    }
}