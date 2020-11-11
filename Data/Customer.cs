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
        public int id_customer { get; set; }

        public string name { get; set; }
        
        public string surname { get; set; }

        public string vat_number { get; set; }

        public int User_id_user { get; set; }


        public User user { get; set; }

        public ICollection<Customer_Order> Customer_Orders { get; set; }
        public ICollection<Adress> Adress { get; set; }

    }
}
