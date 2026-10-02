using Pj_Inventory_System.Domain.Models;
using Pj_Inventory_System.Infrastructure.Repositories;
using Pj_Inventory_System.Application.Services.Base;

namespace Pj_Inventory_System.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repo;

        public SupplierService(ISupplierRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Supplier> GetAll() => _repo.GetAll();
        public Supplier? GetById(int id) => _repo.GetById(id);
        public Supplier? GetByUid(string uid) => _repo.GetByUid(uid);

        public void Create(Supplier entity)
        {
            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(Supplier entity)
        {
            _repo.Update(entity);
            _repo.Save();
        }

        public void Delete(Supplier entity)
        {
            _repo.Delete(entity);
            _repo.Save();
        }
    }
}
