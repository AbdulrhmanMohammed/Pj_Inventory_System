using System.ComponentModel.DataAnnotations.Schema;

namespace Pj_Inventory_System.Models
{
    public class UserFile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FileURL { get; set; } = "";


        [ForeignKey(nameof(Users))]
        public int UserID { get; set; }
        public User? Users { get; set; }


    }
}
