using Pj_Inventory_System.Domain.Models;
using Pj_Inventory_System.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pj_Inventory_System.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly InventorySystemDbContext _db;

        public UserRepository(InventorySystemDbContext db)
        {
            _db = db;
        }

        public User? GetByUsername(string username)
        {
            return _db.Users.FirstOrDefault(
                u => u.Username == username
            );
        }


        public User? GetByEmail(string email)
        {
            return _db.Users.FirstOrDefault(
                u => u.Email == email
            );
        }

        public void Add(User user)
        {
            _db.Users.Add(user);
        }


    }
}
