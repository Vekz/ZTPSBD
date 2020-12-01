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
    public class EditModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        List<String> Types = new List<String> { "User", "Seller", "Admin" };
        public EditModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User user { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            user = await _context.User.FirstOrDefaultAsync(m => m.id_user == id);

            if (user == null)
            {
                return NotFound();
            }

            ViewData["types"] = new SelectList(Types);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["types"] = new SelectList(Types);
                return Page();
            }

            _context.Attach(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(user.id_user))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        
            if(!User.HasClaim("UserType", "Admin")) { return RedirectToPage("/UsersPanel/Index"); }

            return RedirectToPage("./Index");
        }

        private bool userExists(int id)
        {
            return _context.User.Any(e => e.id_user == id);
        }
    }
}
