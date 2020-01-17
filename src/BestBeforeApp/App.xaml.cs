using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using BestBeforeApp.Resources;
using System.Globalization;
using System.Threading.Tasks;
using BestBeforeApp.Shared;
using BestBeforeApp.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BestBeforeApp
{
    public partial class App : Application
    {
        private const string APPCENTERIOS = "c002c4dc-90cd-4b9e-9c62-37f3f9d872a3";
        private const string APPCENTERANDROID = "a530f0ea-d70b-43ac-aa17-ec7a98394c5e";

        public static IServiceProvider ServiceProvider { get; set; }
        public static CultureInfo AppCulture { get; private set; }
        
        public App()
        {
            InitializeComponent();

            //var culture = CrossMultilingual.Current.DeviceCultureInfo;
            AppCulture = new CultureInfo("nl-NL");
            AppResources.Culture = AppCulture;
            MainPage = ServiceProvider.GetService<AppShell>();
        }

        protected override void OnStart()
        {
            //#if !DEBUG
            AppCenter.Start($"ios={APPCENTERIOS};" +
                $"android={APPCENTERANDROID};",
                typeof(Analytics),
                //typeof(Crashes),
                typeof(Distribute));
            //#endif

            var appDbContext = ServiceProvider.GetService<AppDbContext>();
            //appDbContext.Database.EnsureDeleted();
            appDbContext.Database.EnsureCreated();
            //appDbContext.Database.Migrate();

            //#if DEBUG
            
                Task.Run(async () =>
                {
                    if (!await appDbContext.Products.AnyAsync())
                    {
                        var repository = ServiceProvider.GetService<IRepository<Product>>();
                        if (repository != null)
                        {
                            await repository.Add(new Product { Name = "Koekjes", Amount = 1, BestBefore = DateTime.Now.AddMonths(10) });
                            await repository.Add(new Product { Name = "Taart", Amount = 1, BestBefore = DateTime.Now.AddMonths(9) });
                            await repository.Add(new Product { Name = "Pasta", Amount = 1, BestBefore = DateTime.Now.AddMonths(8) });
                            await repository.Add(new Product { Name = "Chocolade", Amount = 1, BestBefore = DateTime.Now.AddMonths(4) });
                            await repository.Add(new Product { Name = "Snoepjes", Amount = 1, BestBefore = DateTime.Now.AddMonths(5) });
                            await repository.Add(new Product { Name = "Wafels", Amount = 1, BestBefore = DateTime.Now.AddDays(7) });
                            await repository.Add(new Product { Name = "Crackers", Amount = 1, BestBefore = DateTime.Now.AddDays(10) });
                            await repository.Add(new Product { Name = "Pannenkoekenbeslag", Amount = 1, BestBefore = DateTime.Now.AddMonths(15) });
                            await repository.Add(new Product { Name = "Pizza", Amount = 1, BestBefore = DateTime.Now.AddMonths(4) });
                            await repository.Add(new Product { Name = "Vlees", Amount = 1, BestBefore = DateTime.Now.AddMonths(3) });
                            await repository.Add(new Product { Name = "Andere koekjes", Amount = 1, BestBefore = DateTime.Now.AddMonths(2) });
                            await repository.Add(new Product { Name = "Havermout", Amount = 6, BestBefore = DateTime.Now.AddYears(1) });
                            await repository.Add(new Product { Name = "Frikandellen", Amount = 1, BestBefore = DateTime.Now });
                        }
                    }

                }).Wait();
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
