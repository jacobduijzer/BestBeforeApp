using System;
using System.ComponentModel;
using BestBeforeApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BestBeforeApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        private AboutViewModel _aboutViewModel;

        public AboutPage()
        {
            InitializeComponent();

            BindingContext = _aboutViewModel = App.ServiceProvider.GetService<AboutViewModel>();
        }
    }
}