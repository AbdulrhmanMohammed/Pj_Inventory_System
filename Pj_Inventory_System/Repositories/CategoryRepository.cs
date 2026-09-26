using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Data;
using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InventorySystemDbContext _db;
        private readonly DbSet<Category> _dbSet;

        public CategoryRepository(InventorySystemDbContext db)
        {
            _db = db;
            _dbSet = db.Set<Category>();
        }

        public IEnumerable<Category> GetAll()
        {
            return _dbSet.ToList();
        }

        public Category? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public Category? GetByUid(string uid)
        {
            return _dbSet.FirstOrDefault(x => x.UID == uid);
        }

        public void Add(Category category)
        {
            _dbSet.Add(category);
        }

        public void Update(Category category)
        {
            _dbSet.Update(category);
        }

        public void Delete(Category category)
        {
            _dbSet.Remove(category);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
