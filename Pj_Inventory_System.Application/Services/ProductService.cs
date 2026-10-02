using Pj_Inventory_System.Domain.Models;
using Pj_Inventory_System.Infrastructure.Repositories;
using Pj_Inventory_System.Application.Services.Base;

namespace Pj_Inventory_System.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Product> GetAll() => _repo.GetAll();
        public Product? GetById(int id) => _repo.GetById(id);
        public Product? GetByUid(string uid) => _repo.GetByUid(uid);

        public void Create(Product entity)
        {
            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(Product entity)
        {
            _repo.Update(entity);
            _repo.Save();
        }

        public void Delete(Product entity)
        {
            _repo.Delete(entity);
            _repo.Save();
        }
    }
}
