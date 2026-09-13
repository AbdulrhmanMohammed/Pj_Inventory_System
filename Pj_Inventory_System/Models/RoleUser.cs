using System.ComponentModel.DataAnnotations.Schema;

namespace Pj_Inventory_System.Models
{
    public class RoleUser
    {
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public Role Role { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
