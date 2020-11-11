using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace ZTPSBD.Data
{
    public class Product
    {

        [Key]
        public int id_product { get; set; }

        [Display(Name = "Nazwa")]
        [Required]
        public string name { get; set; }

        [Display(Name = "Cena")]
        [Required]
        public float price { get; set; }

        public int expiration_date { get; set; }

        public float mass { get; set; }

        public  int id_category {get; set;}
        public Product_Category product_Category { get; set;}
        public ICollection<Product_Order> Product_Orders { get; set; }
      

    }
}
