using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZTPSBD.Data
{
    public class Customer
    {
        [Key]
        [Display(Name = "Client ID")]
        public int id_customer { get; set; }
      
        
        [Display(Name = "Name")]
        [Required]
        public string name { get; set; }

        [Display(Name = "Surname")]
        [Required]
        public string surname { get; set; }

        [Display(Name = "VAT number")]

        public string vat_number { get; set; }

        [Display(Name = "User ID")]
        [Required]
        public int User_id_user { get; set; }


        public User user { get; set; }

        public ICollection<Customer_Order> Customer_Orders { get; set; }
        public ICollection<Adress> Adress { get; set; }

    }
}
