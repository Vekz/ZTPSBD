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
        public int id_category { get; set; }
        public string name { get; set; }

        public ICollection<Product> products; 
    }
}
