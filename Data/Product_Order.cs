using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZTPSBD.Data
{
    public class Product_Order
    {
        [Display(Name = "Order ID")]
        [Required]
        public int Order_id_order { get; set; }

        [Display(Name = "Product ID")]
        [Required]
        public int Product_id_product { get; set; }

        public Order order { get; set; }
        public Product product { get; set; }



    }
}
