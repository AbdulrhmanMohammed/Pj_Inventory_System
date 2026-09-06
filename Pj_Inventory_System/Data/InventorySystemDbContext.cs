using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Data
{
    public class InventorySystemDbContext : DbContext
    {
        public InventorySystemDbContext(DbContextOptions<InventorySystemDbContext> options)
            : base(options)
        {
        }

     
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<StockIn> StockIn { get; set; }
        public DbSet<StockOut> StockOut { get; set; }
    }
}
