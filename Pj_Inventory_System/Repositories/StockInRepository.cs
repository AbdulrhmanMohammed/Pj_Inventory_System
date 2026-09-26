using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Data;
using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Repositories
{
    public class StockInRepository : IStockInRepository
    {
        private readonly InventorySystemDbContext _db;
        private readonly DbSet<StockIn> _dbSet;

        public StockInRepository(InventorySystemDbContext db)
        {
            _db = db;
            _dbSet = db.Set<StockIn>();
        }

        public IEnumerable<StockIn> GetAll()
        {
            return _dbSet
                .Include(x => x.Product)
                .ToList();
        }

        public StockIn? GetById(int id)
        {
            return _dbSet
                .Include(x => x.Product)
                .FirstOrDefault(x => x.StockInID == id);
        }

        public StockIn? GetByUid(string uid)
        {
            return _dbSet
                .Include(x => x.Product)
                .FirstOrDefault(x => x.UID == uid);
        }

        public void Add(StockIn stockIn)
        {
            _dbSet.Add(stockIn);
        }

        public void Update(StockIn stockIn)
        {
            _dbSet.Update(stockIn);
        }

        public void Delete(StockIn stockIn)
        {
            _dbSet.Remove(stockIn);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
