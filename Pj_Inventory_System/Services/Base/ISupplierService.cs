using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Services.Base
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetAll();
        Supplier? GetById(int id);
        Supplier? GetByUid(string uid);

        void Create(Supplier entity);
        void Update(Supplier entity);
        void Delete(Supplier entity);
    }
}
