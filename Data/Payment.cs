using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZTPSBD.Data
{
    public class Payment
    {
        [Key]
        public int id_payment { get; set; }
        public bool paid { get; set; }

        public string method { get; set; }

        public DateTime due_date { get; set; }

        public int Order_id_order { get; set; }

        public Order order { get; set; }
    }
}
