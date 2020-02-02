using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BestBeforeApp.Shared.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumberControl : ContentView
    {   
        public static readonly BindableProperty TitleValueProperty = BindableProperty.Create(
            nameof(TitleValue), typeof(string), typeof(NumberControl), string.Empty);

        public string TitleValue
        {
            get => (string)GetValue(TitleValueProperty);
            set => SetValue(TitleValueProperty, value);
        }

        public static readonly BindableProperty NumberValueProperty = BindableProperty.Create(
            nameof(NumberValue), typeof(string), typeof(NumberControl), string.Empty);

        public string NumberValue
        {
            get => (string)GetValue(NumberValueProperty);
            set => SetValue(NumberValueProperty, value);
        }

        public static readonly BindableProperty NumberDownCommandProperty = BindableProperty.Create(
            nameof(NumberDownCommand), typeof(ICommand), typeof(NumberControl), default(ICommand));

        public ICommand NumberDownCommand
        {
            get => (ICommand)GetValue(NumberDownCommandProperty);
            set => SetValue(NumberDownCommandProperty, value);
        }
        
        public static readonly BindableProperty NumberUpCommandProperty = BindableProperty.Create(
            nameof(NumberUpCommand), typeof(ICommand), typeof(NumberControl), default(ICommand));

        public ICommand NumberUpCommand
        {
            get => (ICommand)GetValue(NumberUpCommandProperty);
            set => SetValue(NumberUpCommandProperty, value);
        }

        public NumberControl() => InitializeComponent();
    }
}
