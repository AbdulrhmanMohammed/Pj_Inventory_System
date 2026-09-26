using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category? GetById(int id);
        Category? GetByUid(string uid);

        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);

        void Save();
    }
}
