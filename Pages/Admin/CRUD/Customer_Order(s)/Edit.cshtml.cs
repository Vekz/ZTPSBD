using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Customer_Order_s_
{
    public class EditModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public EditModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer_Order Customer_Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer_Order = await _context.Customer_Order
                .Include(c => c.customer)
                .Include(c => c.order).FirstOrDefaultAsync(m => m.Customer_id_customer == id);

            if (Customer_Order == null)
            {
                return NotFound();
            }
           ViewData["Customer_id_customer"] = new SelectList(_context.Customer, "id_customer", "id_customer");
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

            _context.Attach(Customer_Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Customer_OrderExists(Customer_Order.Customer_id_customer))
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

        private bool Customer_OrderExists(int id)
        {
            return _context.Customer_Order.Any(e => e.Customer_id_customer == id);
        }
    }
}
