using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pj_Inventory_System.Domain.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;

        public Product Product { get; set; } = null!;
    }
}
