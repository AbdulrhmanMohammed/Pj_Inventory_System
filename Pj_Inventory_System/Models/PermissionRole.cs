using System.ComponentModel.DataAnnotations.Schema;

namespace Pj_Inventory_System.Models
{
    public class PermissionRole
    {
        [ForeignKey("Permissions")]
        public int PermissionsId { get; set; }
        public Permission? Permissions { get; set; }

        [ForeignKey("Roles")]
        public int RolesId { get; set; }
        public Role? Role { get; set; }
    }
}
