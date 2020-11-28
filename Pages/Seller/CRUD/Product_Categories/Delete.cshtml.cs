using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Product_Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public DeleteModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product_Category Product_Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product_Category = await _context.ProductCategory.FirstOrDefaultAsync(m => m.id_category == id);

            if (Product_Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product_Category = await _context.ProductCategory.FindAsync(id);

            if (Product_Category != null)
            {
                _context.ProductCategory.Remove(Product_Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
