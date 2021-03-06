﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Product_Order_s_
{
    public class DeleteModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public DeleteModel(ZTPSBD.Data.ZTPSBDContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product_Order = await _context.Product_Order.FindAsync(id);

            if (Product_Order != null)
            {
                _context.Product_Order.Remove(Product_Order);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
