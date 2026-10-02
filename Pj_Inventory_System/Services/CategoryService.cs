using Pj_Inventory_System.Models;
using Pj_Inventory_System.Repositories;
using Pj_Inventory_System.Services.Base;

namespace Pj_Inventory_System.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Category> GetAll() => _repo.GetAll();
        public Category? GetById(int id) => _repo.GetById(id);
        public Category? GetByUid(string uid) => _repo.GetByUid(uid);

        public void Create(Category entity)
        {
            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(Category entity)
        {
            _repo.Update(entity);
            _repo.Save();
        }

        public void Delete(Category entity)
        {
            _repo.Delete(entity);
            _repo.Save();
        }
    }
}
