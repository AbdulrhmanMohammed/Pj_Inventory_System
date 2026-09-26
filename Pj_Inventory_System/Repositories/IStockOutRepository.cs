using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Repositories
{
    public interface IStockOutRepository
    {
        IEnumerable<StockOut> GetAll();
        StockOut? GetById(int id);
        StockOut? GetByUid(string uid);

        void Add(StockOut stockOut);
        void Update(StockOut stockOut);
        void Delete(StockOut stockOut);

        void Save();
    }
}
