using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Repositories
{
    public interface IStockInRepository
    {
        IEnumerable<StockIn> GetAll();
        StockIn? GetById(int id);
        StockIn? GetByUid(string uid);

        void Add(StockIn stockIn);
        void Update(StockIn stockIn);
        void Delete(StockIn stockIn);

        void Save();
    }
}
