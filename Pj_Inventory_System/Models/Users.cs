namespace Pj_Inventory_System.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Role>? Roles { get; set; } = new List<Role>(); // Navigation property
    }
}
