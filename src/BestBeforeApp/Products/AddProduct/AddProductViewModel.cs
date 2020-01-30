using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using BestBeforeApp.Settings;
using BestBeforeApp.Shared;
using MediatR;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Options;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Plugin.LocalNotification;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace BestBeforeApp.Products.AddProduct
{
    public class AddProductViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;
        private readonly PhotoService _photoService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ITranslator _translator;

        public ICommand TakePhotoCommand { get; }
        public ICommand RemovePhotoCommand { get; }
        public ICommand SaveProductCommand { get; }
        public ICommand SubstractAmountCommand { get; }
        public ICommand AddAmountCommand { get; }

        public AddProductViewModel(
            IMediator mediator,
            PhotoService photoService,
            IOptions<AppSettings> appSettings,
            ITranslator translator)
        {
            _mediator = mediator;
            _photoService = photoService;
            _appSettings = appSettings;
            _translator = translator;

            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
            RemovePhotoCommand = new Command(RemovePhoto, (args) => ProductPhoto != null);
            SaveProductCommand = new AsyncCommand(SaveProduct, (args) => !string.IsNullOrEmpty(Name));
            SubstractAmountCommand = new Command(SubstractAmount);
            AddAmountCommand = new Command(AddAmount);

            SetCleanProduct();
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                (SaveProductCommand as AsyncCommand)?.RaiseCanExecuteChanged();
            }
        }

        private DateTime _bestBefore; 
        public DateTime BestBefore
        {
            get => _bestBefore;
            set
            {
                _bestBefore = value;
                OnPropertyChanged(nameof(BestBefore));
            }
        }

        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        public bool CanRemovePhoto => ProductPhoto != null;

        private ImageSource _productPhoto;
        public ImageSource ProductPhoto
        {
            get => _productPhoto;
            private set 
            {
                _productPhoto = value;
                OnPropertyChanged(nameof(ProductPhoto));
                OnPropertyChanged(nameof(CanRemovePhoto));
                (RemovePhotoCommand as AsyncCommand)?.RaiseCanExecuteChanged();
            }
        }

        private byte[] _imageBytes;

        private void RemovePhoto(object obj)  
        {
            ProductPhoto = null;
            OnPropertyChanged(nameof(ProductPhoto));
        }

        private void SubstractAmount() => Amount = Amount > 1 ? --Amount : Amount;
        private void AddAmount() => Amount = ++Amount;

        private async Task TakePhotoAsync()
        {
            try
            {
                Analytics.TrackEvent($"{this.GetType().Name} - TakePhotoAsync");
                var imageSource = await _photoService.TakePhoto().ConfigureAwait(false);
                if (imageSource != null)
                {
                    _imageBytes = await GetImageStream(imageSource).ConfigureAwait(false);
                    ProductPhoto = ImageSource.FromStream(() => new MemoryStream(_imageBytes));
                    OnPropertyChanged(nameof(ProductPhoto));
                }
                    
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async Task SaveProduct()
        {
            Analytics.TrackEvent($"{this.GetType().Name} - SaveProduct");
            var productId = await SaveProductInDatabase().ConfigureAwait(false);
            ScheduleNotification(productId);
            SetCleanProduct();

            await Shell.Current.GoToAsync("//products").ConfigureAwait(false);
        }

        private void ScheduleNotification(int productId)
        {
            var notification = new NotificationRequest
            {
                NotificationId = productId,
                BadgeNumber = 0,
                Description = string.Format(_translator.Translate("NotificationMessage"), Name, BestBefore.ToString(_translator.Translate("NotificationMessageDateFormat"))),
                Title = _translator.Translate("AppName"),
#if DEBUG
                NotifyTime = DateTime.Now.AddSeconds(5),
#else
                NotifyTime = DateTime.Now.AddDays(_appSettings.Value.DefaultNumberOfDaysToNotifyBeforeExpire),
#endif
                Android =
                {
                    IconName = "ic_launcher"
                }
            };
            NotificationCenter.Current.Show(notification);
        }

        private async Task<int> SaveProductInDatabase()
        {
            IsBusy = true;

            try
            {
                var newProduct = _imageBytes switch
                {
                    null => new Product(Name, BestBefore, Amount),
                    _ => new Product(Name, BestBefore, Amount, _imageBytes)
                };

                return await _mediator.Send(new AddProduct(newProduct)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return 0;
            }
            finally
            {
                IsBusy = false;
            }            
        }

        private void SetCleanProduct()
        {
            Name = string.Empty;
            BestBefore = DateTime.Now.AddMonths(_appSettings.Value.DefaultBestBeforeMonths);
            Amount = _appSettings.Value.DefaultAmount;
            ProductPhoto = null;
        }

        private async Task<byte[]> GetImageStream(Stream imageStream)
        {
            using MemoryStream ms = new MemoryStream();
            await imageStream.CopyToAsync(ms).ConfigureAwait(false);
            return ms.ToArray();
        }
    }
}
