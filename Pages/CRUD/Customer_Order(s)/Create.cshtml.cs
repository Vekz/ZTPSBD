using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Customer_Order_s_
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
        ViewData["Customer_id_customer"] = new SelectList(_context.Customer, "id_customer", "id_customer");
        ViewData["Order_id_order"] = new SelectList(_context.Order, "id_order", "id_order");
            return Page();
        }

        [BindProperty]
        public Customer_Order Customer_Order { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Customer_Order.Add(Customer_Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
