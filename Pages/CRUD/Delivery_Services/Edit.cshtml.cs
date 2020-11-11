using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Delivery_Services
{
    public class EditModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public EditModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Delivery_Service Delivery_Service { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Delivery_Service = await _context.Delivery_Service
                .Include(d => d.order).FirstOrDefaultAsync(m => m.id_deliverman == id);

            if (Delivery_Service == null)
            {
                return NotFound();
            }
           ViewData["Order_id_order"] = new SelectList(_context.Order, "id_order", "id_order");
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

            _context.Attach(Delivery_Service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Delivery_ServiceExists(Delivery_Service.id_deliverman))
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

        private bool Delivery_ServiceExists(int id)
        {
            return _context.Delivery_Service.Any(e => e.id_deliverman == id);
        }
    }
}
