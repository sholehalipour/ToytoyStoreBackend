using ToytoyStoreBackend.Entities;
using ToytoyStoreBackend.DbContextes;
using ToytoyStoreBackend.Entities.Base;
using Microsoft.EntityFrameworkCore;
namespace ToytoyStoreBackend.DbContextes
{
    public class LibraryDB : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data source=DBFiles\LibraryDB.sqlite");
        }


    }
}
