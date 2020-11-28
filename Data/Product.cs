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

        [Display(Name = "Name")]
        [Required]
        public string name { get; set; }

        [Display(Name = "Price")]
        [Required]
        public float price { get; set; }

        [Display(Name = "How long till expiration")]
        public int expiration_date { get; set; }

        [Display(Name = "Weight (in KG)")]
        [Required]
        public float mass { get; set; }

        [Display(Name = "Category")]
        [Required]
        public  int id_category {get; set;}


        public Product_Category product_Category { get; set;}
        public ICollection<Product_Order> Product_Orders { get; set; }
      

    }
}
