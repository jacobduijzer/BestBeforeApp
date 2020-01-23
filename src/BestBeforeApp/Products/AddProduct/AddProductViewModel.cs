using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MediatR;
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
        public ICommand SaveProductAndNextCommand { get; }
        public ICommand SaveProductAndNavigateCommand { get; }

        public AddProductViewModel(IMediator mediator, PhotoService photoService)
        {
            _mediator = mediator;
            _photoService = photoService;

            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
            RemovePhotoCommand = new Xamarin.Forms.Command(RemovePhoto);
            SaveProductAndNavigateCommand = new AsyncCommand(SaveProductAndNavigate);
        }

        public ImageSource ProductPhoto { get; private set; }

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

        private void RemovePhoto(object obj)  
        {
            ProductPhoto = null;
            OnPropertyChanged(nameof(ProductPhoto));
        }

        private async Task TakePhotoAsync()
        {
            try
            {
                var imageSource = await _photoService.TakePhoto().ConfigureAwait(false);
                if (imageSource != null)
                {
                    ProductPhoto = imageSource;
                    OnPropertyChanged(nameof(ProductPhoto));
                }
                    
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async Task SaveProductAndNavigate()
        {
            IsBusy = true;

            try
            {
                await SaveProduct().ConfigureAwait(false);
                await Shell.Current.GoToAsync("//products");
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

        private async Task SaveProduct() =>
            await _mediator
                .Publish(new AddProduct(new Product(Name, BestBefore, Amount)))
                .ConfigureAwait(false);
    }
}
