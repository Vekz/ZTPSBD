using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.UserPanel
{
    public class IndexModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;
        public int IdUser { get; set; }
        public int IdCustomer { get; set; }

        public IndexModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            string login = String.Empty;
            List<ZTPSBD.Data.User> users = await _context.User.ToListAsync();
            List<ZTPSBD.Data.Customer> customers = await _context.Customer.ToListAsync();

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            login = User.Identity.Name;
            IdUser = users.Find(user => user.login.Equals(login)).id_user;
            IdCustomer = customers.Find(customer => customer.User_id_user == IdUser).id_customer;
        }
    }
}
