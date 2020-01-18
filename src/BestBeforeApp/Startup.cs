using System;
using System.IO;
using System.Reflection;
using BestBeforeApp.AddProduct;
using BestBeforeApp.Helpers;
using BestBeforeApp.ProductDetails;
using BestBeforeApp.Products;
using BestBeforeApp.Shared;
using BestBeforeApp.ViewModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xamarin.Essentials;

namespace BestBeforeApp
{
    public class Startup
    {
        public static App Init(Action<HostBuilderContext, IServiceCollection> nativeConfigureServices)
        {
            var systemDir = FileSystem.CacheDirectory;
            Utils.ExtractSaveResource("BestBeforeApp.appsettings.json", systemDir);
            var fullConfig = Path.Combine(systemDir, "BestBeforeApp.appsettings.json");

            var host = new HostBuilder()
                            .ConfigureHostConfiguration(c =>
                            {
                                c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                                c.AddJsonFile(fullConfig);
                            })
                            .ConfigureServices((c, x) =>
                            {
                                nativeConfigureServices(c, x);
                                ConfigureServices(c, x);
                            })
                            .ConfigureLogging(l => l.AddConsole(o =>
                            {
                                o.DisableColors = true;
                            }))
                            .Build();

            App.ServiceProvider = host.Services;

            return App.ServiceProvider.GetService<App>();
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services) =>
            services.AddSingleton<AppShell>(factory => new AppShell())
                .AddDbContext<AppDbContext>()
                .AddScoped<IRepository<Product>, ProductRepository>()
                .AddScoped<ProductsViewModel>()
                .AddScoped<ProductDetailsViewModel>()
                .AddScoped<AddProductViewModel>()
                .AddScoped<AboutViewModel>()
                .AddScoped<PhotoService>()
                .AddMediatR(cfg => cfg.AsScoped(), typeof(AddProductHandler).GetTypeInfo().Assembly)
                .AddSingleton<App>();
    }
}
