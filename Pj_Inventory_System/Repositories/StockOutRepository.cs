using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Data;
using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Repositories
{
    public class StockOutRepository : IStockOutRepository
    {
        private readonly InventorySystemDbContext _db;
        private readonly DbSet<StockOut> _dbSet;

        public StockOutRepository(InventorySystemDbContext db)
        {
            _db = db;
            _dbSet = db.Set<StockOut>();
        }

        public IEnumerable<StockOut> GetAll()
        {
            return _dbSet
                .Include(x => x.Product)
                .ToList();
        }

        public StockOut? GetById(int id)
        {
            return _dbSet
                .Include(x => x.Product)
                .FirstOrDefault(x => x.StockOutID == id);
        }

        public StockOut? GetByUid(string uid)
        {
            return _dbSet
                .Include(x => x.Product)
                .FirstOrDefault(x => x.UID == uid);
        }

        public void Add(StockOut stockOut)
        {
            _dbSet.Add(stockOut);
        }

        public void Update(StockOut stockOut)
        {
            _dbSet.Update(stockOut);
        }

        public void Delete(StockOut stockOut)
        {
            _dbSet.Remove(stockOut);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
