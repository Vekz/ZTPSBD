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
        [Display(Name = "ID_Płatności")]
        public int id_payment { get; set; }
        [Display(Name = "Czy zapłacone?")]
        [Required]
        public bool paid { get; set; }

        [Display(Name = "Metoda Płatności")]
        public string method { get; set; }
        [Display(Name = "Data realizacji")]
        public DateTime due_date { get; set; }
        [Display(Name = "ID Zamówienia")]
        [Required]
        public int Order_id_order { get; set; }

        public Order order { get; set; }
    }
}
