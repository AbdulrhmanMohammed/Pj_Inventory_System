using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Repositories
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetAll();
        Supplier? GetById(int id);
        Supplier? GetByUid(string uid);

        void Add(Supplier supplier);
        void Update(Supplier supplier);
        void Delete(Supplier supplier);

        void Save();
    }
}
