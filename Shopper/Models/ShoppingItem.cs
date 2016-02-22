using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Models
{
    public class ShoppingItem
    {
        [Required]
        [Range(1,int.MaxValue,ErrorMessage="Quantity must be at  least 1")]
        public int Quantity { get; set; }

        [Required(ErrorMessage="No Item included")]
        public Item Item { get; set; }
        

        public ShoppingItem()
        {
            
        }
        public ShoppingItem(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

       
    }
}
