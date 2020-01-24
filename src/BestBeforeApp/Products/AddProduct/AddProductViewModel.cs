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
        public ICommand SaveProductAndStartNewCommand { get; }
        public ICommand SaveProductAndNavigateCommand { get; }

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
            RemovePhotoCommand = new Xamarin.Forms.Command(RemovePhoto);
            SaveProductAndStartNewCommand = new AsyncCommand(SaveProductAndNew);
            SaveProductAndNavigateCommand = new AsyncCommand(SaveProductAndNavigate);

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

        private ImageSource _productPhoto;
        public ImageSource ProductPhoto
        {
            get => _productPhoto;
            private set 
            {
                _productPhoto = value;
                OnPropertyChanged(nameof(ProductPhoto));
            }
        }

        private byte[] _imageBytes;

        private void RemovePhoto(object obj)  
        {
            ProductPhoto = null;
            OnPropertyChanged(nameof(ProductPhoto));
        }

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

        private async Task SaveProductAndNew()
        {
            Analytics.TrackEvent($"{this.GetType().Name} - SaveProductAndNew");
            await SaveProductAndScheduleNotification().ConfigureAwait(false);
            SetCleanProduct();
        }

        private void SetCleanProduct()
        {
            Name = string.Empty;
            BestBefore = DateTime.Now.AddMonths(_appSettings.Value.DefaultBestBeforeMonths);
            Amount = _appSettings.Value.DefaultAmount;
            ProductPhoto = null;
        }

        private async Task SaveProductAndNavigate()
        {
            Analytics.TrackEvent($"{this.GetType().Name} - SaveProductAndNavigate");
            await SaveProductAndScheduleNotification().ConfigureAwait(false);

            SetCleanProduct();

            await Shell.Current.GoToAsync("//products").ConfigureAwait(false);
        }

        private async Task SaveProductAndScheduleNotification()
        {
            var productId = await SaveProduct().ConfigureAwait(false);
            ScheduleNotification(productId);
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

        private async Task<int> SaveProduct()
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

        private async Task<byte[]> GetImageStream(Stream imageStream)
        {
            using MemoryStream ms = new MemoryStream();
            await imageStream.CopyToAsync(ms).ConfigureAwait(false);
            return ms.ToArray();
        }
    }
}
