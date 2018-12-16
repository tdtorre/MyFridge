using Microsoft.EntityFrameworkCore;
using MyFridge.Data.Model;

namespace MyFridge.Data
{
    public class MyFridgeContext : DbContext
    {
        public MyFridgeContext(DbContextOptions<MyFridgeContext> contextOptions) : base(contextOptions) { }

        public DbSet<FridgeItem> FridgeItems { get; set; }

        public DbSet<ShoppingItem> ShoppingItems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
