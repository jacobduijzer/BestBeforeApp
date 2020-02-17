using System.Windows.Input;
using BestBeforeApp.Shared;
using MediatR;
using Microsoft.Extensions.Options;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace BestBeforeApp.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IPreferenceService _preferenceService;

        public ICommand MinCommand { get; private set; }
        public ICommand PlusCommand { get; private set; }

        public SettingsViewModel(
            IMediator mediator,
            IPreferenceService preferenceService)
        {
            _mediator = mediator;
            _preferenceService = preferenceService;

            UseNotifications = _preferenceService.UseNotifications;
            NumberOfDaysBeforeExpirationDate = _preferenceService.NumberOfDaysBeforeExpirationDate;

            MinCommand = new Command(DoMinus, () => _preferenceService.NumberOfDaysBeforeExpirationDate > 1);
            PlusCommand = new Command(DoPlus);
        }

        private bool _useNotifications;
        public bool UseNotifications
        {
            get => _useNotifications;
            set
            {
                _useNotifications = value;
                _preferenceService.UseNotifications = value;
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
                _preferenceService.NumberOfDaysBeforeExpirationDate = value;
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
