using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZTPSBD.Data
{
    public class Product_Order
    {
        public int Order_id_order { get; set; }

        public Order order { get; set; }

        public int Product_id_product { get; set; }

        public Product product { get; set; }



    }
}
