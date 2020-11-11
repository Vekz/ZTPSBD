using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZTPSBD.Data
{
    public class Customer_Order
    {

        public int Customer_id_customer { get; set; }

        public int Order_id_order { get; set; }


        public Customer customer { get; set; }

        public Order order {get; set;} 



    }
}
