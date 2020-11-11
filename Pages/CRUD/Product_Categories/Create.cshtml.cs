using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Product_Categories
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
            return Page();
        }

        [BindProperty]
        public Product_Category Product_Category { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProductCategory.Add(Product_Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
