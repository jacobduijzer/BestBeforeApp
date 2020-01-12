using System;
using System.IO;
using BestBeforeApp.Helpers;
using BestBeforeApp.Models;
using BestBeforeApp.Products;
using BestBeforeApp.Services;
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


        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddSingleton<AppShell>(factory => new AppShell());
            services.AddTransient<IDataStore<Item>, MockDataStore>();
            services.AddTransient<IProductsService, MockProductsService>();
            services.AddTransient<ProductsViewModel>();
            services.AddSingleton<App>();
        }
    }
}
