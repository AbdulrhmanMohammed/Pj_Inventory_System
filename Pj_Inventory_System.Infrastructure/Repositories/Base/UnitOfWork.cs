using Pj_Inventory_System.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pj_Inventory_System.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventorySystemDbContext _db;

        public UnitOfWork(InventorySystemDbContext db)
        {
            _db = db;
            
            

            UserRepo = new UserRepository(db);
           
        }

       

       
        public IUserRepository UserRepo { get; }

       

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

