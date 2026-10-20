using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pj_Inventory_System.Infrastructure.Repositories.Base
{
    public interface IUnitOfWork
    {
   

        
        IUserRepository UserRepo { get; }

       
        void Save();
    }
}
