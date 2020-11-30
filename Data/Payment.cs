using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZTPSBD.Data
{
    public class Payment
    {
        [Key]
        [Display(Name = "payment")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int id_payment { get; set; }
        [Display(Name = "Is paid?")]
        [Required]
        public bool paid { get; set; }

        [Display(Name = "Payment method")]
        public string method { get; set; }
        [Display(Name = "Date of payment")]
        public DateTime due_date { get; set; }
        [Display(Name = "Order ID")]
        [Required]
        public int Order_id_order { get; set; }

        public Order order { get; set; }
    }
}
