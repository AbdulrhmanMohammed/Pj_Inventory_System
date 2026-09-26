using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        Product? GetByUid(string uid);

        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);

        void Save();
    }
}
