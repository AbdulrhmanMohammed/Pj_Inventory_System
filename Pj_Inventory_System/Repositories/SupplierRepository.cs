using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Data;
using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly InventorySystemDbContext _db;
        private readonly DbSet<Supplier> _dbSet;

        public SupplierRepository(InventorySystemDbContext db)
        {
            _db = db;
            _dbSet = db.Set<Supplier>();
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _dbSet.ToList();
        }

        public Supplier? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public Supplier? GetByUid(string uid)
        {
            return _dbSet.FirstOrDefault(x => x.UID == uid);
        }

        public void Add(Supplier supplier)
        {
            _dbSet.Add(supplier);
        }

        public void Update(Supplier supplier)
        {
            _dbSet.Update(supplier);
        }

        public void Delete(Supplier supplier)
        {
            _dbSet.Remove(supplier);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
