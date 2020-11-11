using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZTPSBD.Data
{
    public class Delivery_Service
    {

        [Key]
        public int id_deliverman { get; set; }

        public DateTime delivery_date { get; set; }

        public int Order_id_order { get; set; }

        public Order order { get; set; }

    }
}
