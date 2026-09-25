namespace Pj_Inventory_System.Models
{
    public class Role
    {

        public int Id { get; set; }

        public string UID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "";

        public ICollection<Permission>? Permissions { get; set; } = new List<Permission>(); // Navigation property
        public ICollection<User>? Users { get; set; } = new List<User>(); // Navigation property
    }
}
