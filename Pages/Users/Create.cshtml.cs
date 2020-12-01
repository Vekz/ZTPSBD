using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Users
{
    public class CreateModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;
        List<String> Types = new List<String> { "User", "Seller", "Admin" };

        public CreateModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["types"] = new SelectList(Types);
            return Page();
        }

        [BindProperty]
        public User user { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["types"] = new SelectList(Types, user.User_Type);
                return Page();
            }

            if(!User.HasClaim("UserType", "Admin"))
            {
                user.User_Type = "User";
            }
            List<User> list = await _context.User.ToListAsync();
            user.id_user= list.Count() > 0 ? list.Last().id_user + 1 : 1;

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            TempData["userId"] = user.id_user;
            return RedirectToPage("/Customers/Create");
        }
    }
}
