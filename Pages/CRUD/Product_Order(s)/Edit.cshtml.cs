using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Product_Order_s_
{
    public class EditModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public EditModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product_Order Product_Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product_Order = await _context.Product_Order
                .Include(p => p.order)
                .Include(p => p.product).FirstOrDefaultAsync(m => m.Order_id_order == id);

            if (Product_Order == null)
            {
                return NotFound();
            }
           ViewData["Order_id_order"] = new SelectList(_context.Order, "id_order", "id_order");
           ViewData["Product_id_product"] = new SelectList(_context.Product, "id_product", "name");
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

            _context.Attach(Product_Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Product_OrderExists(Product_Order.Order_id_order))
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

        private bool Product_OrderExists(int id)
        {
            return _context.Product_Order.Any(e => e.Order_id_order == id);
        }
    }
}
