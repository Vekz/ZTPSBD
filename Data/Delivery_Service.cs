using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZTPSBD.Data
{
    public class Delivery_Service
    {

        [Key]
        [Display(Name = "Delivery ID")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int id_deliverman { get; set; }

        [Display(Name = "Date of delivery")]
        [Required]
        public DateTime delivery_date { get; set; }
        [Required]
        public int Order_id_order { get; set; }

        public Order order { get; set; }

    }
}
