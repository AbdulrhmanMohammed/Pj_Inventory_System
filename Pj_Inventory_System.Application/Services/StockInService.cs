using Pj_Inventory_System.Domain.Models;
using Pj_Inventory_System.Infrastructure.Repositories;
using Pj_Inventory_System.Application.Services.Base;

namespace Pj_Inventory_System.Application.Services
{
    public class StockInService : IStockInService
    {
        private readonly IStockInRepository _repo;

        public StockInService(IStockInRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<StockIn> GetAll() => _repo.GetAll();
        public StockIn? GetById(int id) => _repo.GetById(id);
        public StockIn? GetByUid(string uid) => _repo.GetByUid(uid);

        public void Create(StockIn entity)
        {
            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(StockIn entity)
        {
            _repo.Update(entity);
            _repo.Save();
        }

        public void Delete(StockIn entity)
        {
            _repo.Delete(entity);
            _repo.Save();
        }
    }
}
