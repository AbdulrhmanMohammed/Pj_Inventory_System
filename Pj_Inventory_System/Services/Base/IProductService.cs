using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Services.Base
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        Product? GetByUid(string uid);

        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
    }
}
