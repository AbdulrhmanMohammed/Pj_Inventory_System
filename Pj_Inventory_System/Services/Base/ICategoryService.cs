using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Services.Base
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category? GetById(int id);
        Category? GetByUid(string uid);

        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
