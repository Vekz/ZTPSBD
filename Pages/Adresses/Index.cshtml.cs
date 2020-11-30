using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Adresses
{
    public class IndexModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public IndexModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public IList<Adress> Adress { get;set; }

        public async Task OnGetAsync()
        {
            if (User.HasClaim("UserType", "User") || User.HasClaim("UserType", "Seller"))
            {
                string login = String.Empty;
                List<ZTPSBD.Data.User> users = await _context.User.ToListAsync();

                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                login = User.Identity.Name;
                int Id = users.Find(user => user.login.Equals(login)).id_user;

                int customerId = _context.Customer.Where(c => c.User_id_user == Id).SingleOrDefault().id_customer;
                Adress = _context.Adress.Where(a => a.Customer_Id_customer == customerId).ToList();
                TempData["customerId"] = customerId;
            }
            else
            {
                Adress = await _context.Adress
                    .Include(a => a.customer).ToListAsync();
            }
        }
    }
}
