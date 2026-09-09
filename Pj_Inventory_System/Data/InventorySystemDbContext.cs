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

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<PermissionRole> PermissionRoles { get; set; }

        public DbSet<RoleUser> RoleUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PermissionRole>()
                .HasKey(pr => new
                {
                    pr.RolesId,
                    pr.PermissionsId
                });


            modelBuilder.Entity<RoleUser>()
                .HasKey(ru => new
                {
                    ru.RoleId,
                    ru.UserId

                });

        }


    }
}
