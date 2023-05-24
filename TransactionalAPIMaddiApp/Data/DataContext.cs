using Microsoft.EntityFrameworkCore;
using TransactionalAPIMaddiApp.Data.Entities;

namespace TransactionalAPIMaddiApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<AssetsImage> tblAssetsImage { get; set; }
        public DbSet<Restaurant> tblRestaurant { get; set; }
        public DbSet<User> tblUser { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetsImage>().HasIndex(c => c.Id).IsUnique();

            modelBuilder.Entity<User>().HasIndex(c => c.Id).IsUnique();

            modelBuilder.Entity<Restaurant>().HasIndex("Id", "UserId").IsUnique();

            modelBuilder.Entity<Restaurant>().HasIndex("Id", "AssetsImageId").IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}