using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Product_Order_s_
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
        ViewData["Order_id_order"] = new SelectList(_context.Order, "id_order", "id_order");
        ViewData["Product_id_product"] = new SelectList(_context.Product, "id_product", "name");
            return Page();
        }

        [BindProperty]
        public Product_Order Product_Order { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Product_Order.Add(Product_Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
