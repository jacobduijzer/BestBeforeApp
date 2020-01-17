using System;
using System.Windows.Input;
using BestBeforeApp.Products;
using BestBeforeApp.Shared;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BestBeforeApp.ViewModels
{
    public class AboutViewModel
    {
        private readonly IRepository<Product> _productRepository;

        public AboutViewModel(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
            //Title = "About";
            OpenWebCommand = new Command(async () => await _productRepository.Add(new Product()
            {
                Name = "Koekjes", Amount = 1, BestBefore = DateTime.Now.AddMonths(10)
            }));
        }

        public ICommand OpenWebCommand { get; }
    }
}