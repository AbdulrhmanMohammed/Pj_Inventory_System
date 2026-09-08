using System.Data;

namespace Pj_Inventory_System.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";



        public ICollection<Role>? Roles { get; set; } = new List<Role>(); // Navigation property

    }
}
