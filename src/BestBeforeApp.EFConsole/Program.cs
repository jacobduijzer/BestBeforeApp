using System;
using System.Threading.Tasks;
using BestBeforeApp.Helpers;
using BestBeforeApp.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BestBeforeApp.EFConsole
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IDatabaseFileHelper, DatabaseFileHelper>()
                //.AddSingleton<AppDbContext>()
                .AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=KnowledgeDatabase.db"))
                .BuildServiceProvider();

            //var appDbContext = serviceProvider.GetService<AppDbContext>();
            //using (var context = new AppDbContext(default))
            //{

            //    //var std = new Student()
            //    //{
            //    //    Name = "Bill"
            //    //};

            //    //context.Students.Add(std);
            //    //context.SaveChanges();
            //}

        }
    }
}
