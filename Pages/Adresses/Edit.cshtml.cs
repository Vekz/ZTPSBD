using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Adresses
{
    public class EditModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public EditModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Adress Adress { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Adress = await _context.Adress
                .Include(a => a.customer).FirstOrDefaultAsync(m => m.id_adress == id);

            if (Adress == null)
            {
                return NotFound();
            }

            if (!User.HasClaim("UserType", "User") || User.HasClaim("UserType", "Seller"))
            {
                ViewData["Customer_Id_customer"] = new SelectList(_context.Customer, "id_customer", "id_customer");
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (User.HasClaim("UserType", "User") || User.HasClaim("UserType", "Seller"))
            {
                Adress.Customer_Id_customer = (int)TempData["customerId"];
            }
            _context.Attach(Adress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdressExists(Adress.id_adress))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AdressExists(int id)
        {
            return _context.Adress.Any(e => e.id_adress == id);
        }
    }
}
