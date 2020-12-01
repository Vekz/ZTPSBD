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
        [Display(Name = "User ID")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int id_user { get; set; }
        [Required]
        [Display(AutoGenerateField = false, Name = "Password")]
        public string password { get; set; }
        [Required]
        [Display(Name = "Login")]
        public string login { get; set; }

        public string User_Type { get; set; }

        [EmailAddress]
        [Required]
        [Display(Name = "Address e-mail")]
        public string email_address { get; set; }

        public Customer customer { get; set; }

    }
}
