using BestBeforeApp.Helpers;
using BestBeforeApp.Products;
using Microsoft.AppCenter;
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
            try
            {
                Database.EnsureCreated();
                Database.Migrate();
            }
            catch (System.Exception ex)
            {
                Crashes.TrackError(ex);
                throw;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var path = _dbFileHelper.GetLocalFilePath("bestbeforeapp.db");
            options.UseSqlite($"Data Source={path}");
        }

        #region use this for Ef Migrations

        //public AppDbContext(DbContextOptions<AppDbContext> options)
        //    : base(options)
        //{
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        //    options.UseSqlite($"Data Source=somefakefile.db");

        #endregion

        public DbSet<Product> Products { get; set; }
    }
}