using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public DetailsModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product
                .Include(p => p.product_Category).FirstOrDefaultAsync(m => m.id_product == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
