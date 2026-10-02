using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Services.Base
{
    public interface IStockOutService
    {
        IEnumerable<StockOut> GetAll();
        StockOut? GetById(int id);
        StockOut? GetByUid(string uid);

        void Create(StockOut entity);
        void Update(StockOut entity);
        void Delete(StockOut entity);
    }
}
