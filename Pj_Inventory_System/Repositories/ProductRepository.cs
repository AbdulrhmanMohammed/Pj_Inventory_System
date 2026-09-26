using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Data;
using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventorySystemDbContext _db;
        private readonly DbSet<Product> _dbSet;

        public ProductRepository(InventorySystemDbContext db)
        {
            _db = db;
            _dbSet = db.Set<Product>();
        }

        public IEnumerable<Product> GetAll()
        {
            return _dbSet
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .ToList();
        }

        public Product? GetById(int id)
        {
            return _dbSet
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .FirstOrDefault(x => x.ProductID == id);
        }

        public Product? GetByUid(string uid)
        {
            return _dbSet
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .FirstOrDefault(x => x.UID == uid);
        }

        public void Add(Product product)
        {
            _dbSet.Add(product);
        }

        public void Update(Product product)
        {
            _dbSet.Update(product);
        }

        public void Delete(Product product)
        {
            _dbSet.Remove(product);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
