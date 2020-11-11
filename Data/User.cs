using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
namespace ZTPSBD.Data
{
    public class User
    {
        [Key]
        public int id_user { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string login { get; set; }
        [EmailAddress]
        public string email_address { get; set; }

        public Customer customer { get; set; }




    }
}
