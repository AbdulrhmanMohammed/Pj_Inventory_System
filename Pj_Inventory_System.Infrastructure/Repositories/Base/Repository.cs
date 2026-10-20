using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pj_Inventory_System.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly InventorySystemDbContext _db;
        private readonly DbSet<T> _dbSet;
        public Repository(InventorySystemDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
            //-------------
        }

        public void Add(T employee)
        {
            _dbSet.Add(employee);
        }

        public void Delete(T employee)
        {
            _dbSet.Remove(employee);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }



        public void Update(T employee)
        {
            _dbSet.Update(employee);
        }
    }
}
