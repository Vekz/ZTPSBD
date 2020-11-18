using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace ZTPSBD.Data
{
    public class Customer_Order
    {
        [Display(Name = "ID_Klienta")]
        [Required]
        public int Customer_id_customer { get; set; }

        [Display(Name = "ID_Zamówienia")]
        [Required]
        public int Order_id_order { get; set; }


        public Customer customer { get; set; }

        public Order order {get; set;} 



    }
}
