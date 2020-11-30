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
        public int Id { get; set; }

        public IndexModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            string login = String.Empty;
            List<ZTPSBD.Data.User> users = await _context.User.ToListAsync();

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            login = User.Identity.Name;
            Id = users.Find(user => user.login.Equals(login)).id_user;
        }
    }
}
