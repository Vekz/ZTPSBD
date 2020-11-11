using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZTPSBD.Data
{
    public class Order
    {
        [Key]
        public int id_order { get; set; }

        public DateTime due_date { get; set; }
        
        public int Payment_id_payment
        { get; set; }

        public int Delivery_Service_id_deliveryman { get; set; }


        public Payment payment;
        public Delivery_Service deliveryService { get; set; }

        public  ICollection<Customer_Order> Customer_Orders { get; set; }
        public  ICollection<Product_Order> Product_Orders { get; set; }

    }
}
