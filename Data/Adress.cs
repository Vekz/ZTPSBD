using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace ZTPSBD.Data
{
    public class Adress
    {
        [Key]
        [Display(Name = "Address ID")]
        public int id_adress { get; set; }

        [Display(Name = "Street")]
        [Required]
        public string street { get; set; }

        [Display(Name = "City")]
        [Required]
        public string city { get; set; }

        [Display(Name = "Street Number")]
        [Required]
        public int street_no { get; set; }


        [Required]
        public int Customer_Id_customer { get; set;}

        public Customer customer { get; set; }

    }
}
