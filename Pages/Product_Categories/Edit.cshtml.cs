﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Product_Categories
{
    public class EditModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public EditModel(ZTPSBD.Data.ZTPSBDContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Product_Category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Product_CategoryExists(Product_Category.id_category))
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

        private bool Product_CategoryExists(int id)
        {
            return _context.ProductCategory.Any(e => e.id_category == id);
        }
    }
}
