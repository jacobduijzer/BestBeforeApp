using System;
using MediatR;
using MvvmHelpers;
using Xamarin.Essentials;

namespace BestBeforeApp.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;

        public SettingsViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
