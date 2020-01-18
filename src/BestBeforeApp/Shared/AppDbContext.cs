using BestBeforeApp.Helpers;
using BestBeforeApp.Products;
using Microsoft.EntityFrameworkCore;

namespace BestBeforeApp.Shared
{
    public class AppDbContext : DbContext
    {
        private readonly IDatabaseFileHelper _dbFileHelper;

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions, IDatabaseFileHelper dbFileHelper) :
            base(dbContextOptions)
        {
            _dbFileHelper = dbFileHelper;
            Database.Migrate();
        }

        //public AppDbContext(DbContextOptions<AppDbContext> options)
        //    : base(options)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var path = _dbFileHelper.GetLocalFilePath("bestbeforeapp.db");
            options.UseSqlite($"Data Source={path}");
        }

        public DbSet<Product> Products { get; set; }
    }
}