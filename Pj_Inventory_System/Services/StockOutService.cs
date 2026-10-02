using Pj_Inventory_System.Models;
using Pj_Inventory_System.Repositories;
using Pj_Inventory_System.Services.Base;

namespace Pj_Inventory_System.Services
{
    public class StockOutService : IStockOutService
    {
        private readonly IStockOutRepository _repo;

        public StockOutService(IStockOutRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<StockOut> GetAll() => _repo.GetAll();
        public StockOut? GetById(int id) => _repo.GetById(id);
        public StockOut? GetByUid(string uid) => _repo.GetByUid(uid);

        public void Create(StockOut entity)
        {
            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(StockOut entity)
        {
            _repo.Update(entity);
            _repo.Save();
        }

        public void Delete(StockOut entity)
        {
            _repo.Delete(entity);
            _repo.Save();
        }
    }
}
