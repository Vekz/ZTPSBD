﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Payments
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
            return Page();
        }

        [BindProperty]
        public Payment Payment { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            List<Payment> list = await _context.Payment.ToListAsync();
            Payment.id_payment = list.Count() > 0 ? list.Last().id_payment + 1 : 1;

            _context.Payment.Add(Payment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
