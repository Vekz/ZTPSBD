using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Users
{
    public class CreateModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public CreateModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            List<String> Types = new List<String> { "User", "Seller", "Admin" };
            ViewData["types"] = new SelectList(Types);
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                List<String> Types = new List<String> { "User", "Seller", "Admin" };
                ViewData["types"] = new SelectList(Types, User.User_Type);
                return Page();
            }

            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
