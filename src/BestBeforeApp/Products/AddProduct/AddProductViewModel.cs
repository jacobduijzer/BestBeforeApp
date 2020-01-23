using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using MediatR;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace BestBeforeApp.Products.AddProduct
{
    public class AddProductViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;
        private readonly PhotoService _photoService;
        
        public ICommand TakePhotoCommand { get; }
        public ICommand RemovePhotoCommand { get; }
        public ICommand SaveProductAndStartNewCommand { get; }
        public ICommand SaveProductAndNavigateCommand { get; }

        public AddProductViewModel(IMediator mediator, PhotoService photoService)
        {
            _mediator = mediator;
            _photoService = photoService;

            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
            RemovePhotoCommand = new Xamarin.Forms.Command(RemovePhoto);
            SaveProductAndStartNewCommand = new AsyncCommand(SaveProductAndNew);
            SaveProductAndNavigateCommand = new AsyncCommand(SaveProductAndNavigate);
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

        private DateTime _bestBefore = DateTime.Now.AddMonths(3);
        public DateTime BestBefore
        {
            get => _bestBefore;
            set
            {
                _bestBefore = value;
                OnPropertyChanged(nameof(BestBefore));
            }
        }

        private int _amount = 1;
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
            await SaveProduct().ConfigureAwait(false);
            Name = string.Empty;
            BestBefore = DateTime.Now.AddMonths(3);
            Amount = 1;
            ProductPhoto = null;
        }

        private async Task SaveProductAndNavigate()
        {
            Analytics.TrackEvent($"{this.GetType().Name} - SaveProductAndNavigate");
            await SaveProduct().ConfigureAwait(false);
            await Shell.Current.GoToAsync("//products").ConfigureAwait(false);
        }

        private async Task SaveProduct()
        {
            IsBusy = true;

            try
            {
                var newProduct = _imageBytes switch
                {
                    null => new Product(Name, BestBefore, Amount),
                    _ => new Product(Name, BestBefore, Amount, _imageBytes)
                };

                await _mediator.Publish(new AddProduct(newProduct)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
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
