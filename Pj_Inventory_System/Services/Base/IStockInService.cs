using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Services.Base
{
    public interface IStockInService
    {
        IEnumerable<StockIn> GetAll();
        StockIn? GetById(int id);
        StockIn? GetByUid(string uid);

        void Create(StockIn entity);
        void Update(StockIn entity);
        void Delete(StockIn entity);
    }
}
