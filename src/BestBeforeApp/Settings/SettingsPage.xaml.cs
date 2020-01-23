using BestBeforeApp.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace BestBeforeApp.Settings
{
    public partial class SettingsPage : BasePage
    {
        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = App.ServiceProvider.GetService<SettingsViewModel>();
        }
    }
}
