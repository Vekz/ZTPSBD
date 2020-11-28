using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ZTPSBD.Data
{
    public class Product_Category
    {
        [Key]
        [Display(Name = "Category ID")]
        [Required]
        public int id_category { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string name { get; set; }

        public ICollection<Product> products; 
    }
}
