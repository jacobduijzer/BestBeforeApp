using System;
using System.Windows.Input;
using MediatR;
using Microsoft.Extensions.Options;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Essentials;

namespace BestBeforeApp.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;
        private readonly IOptions<AppSettings> _appSettings;

        public ICommand MinCommand { get; private set; }
        public ICommand PlusCommand { get; private set; }

        public SettingsViewModel(
            IMediator mediator,
            IOptions<AppSettings> appSettings)
        {
            _mediator = mediator;
            _appSettings = appSettings;

            UseNotifications = Preferences.Get(nameof(UseNotifications), _appSettings.Value.UseNotifications);
            NumberOfDaysBeforeExpirationDate = Preferences.Get(nameof(NumberOfDaysBeforeExpirationDate), _appSettings.Value.DefaultNumberOfDaysToNotifyBeforeExpire);

            MinCommand = new Command(DoMinus);
            PlusCommand = new Command(DoPlus);
        }

        private bool _useNotifications;
        public bool UseNotifications
        {
            get => _useNotifications;
            set
            {
                _useNotifications = value;
                Preferences.Set(nameof(UseNotifications), value);
                OnPropertyChanged(nameof(UseNotifications));
            }
        }

        private int _numberOfDaysBeforeExpirationDate;
        public int NumberOfDaysBeforeExpirationDate
        {
            get => _numberOfDaysBeforeExpirationDate;
            set
            {
                _numberOfDaysBeforeExpirationDate = value;
                Preferences.Set(nameof(NumberOfDaysBeforeExpirationDate), value);
                OnPropertyChanged(nameof(NumberOfDaysBeforeExpirationDate));
            }
        }

        private void DoPlus() => NumberOfDaysBeforeExpirationDate++;

        private void DoMinus()
        {
            if (NumberOfDaysBeforeExpirationDate > 1)
                NumberOfDaysBeforeExpirationDate--;
        }
    }
}
