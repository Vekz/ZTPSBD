﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Adresses
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
            if (!User.HasClaim("UserType", "User") || User.HasClaim("UserType", "Seller"))
            {
                ViewData["Customer_Id_customer"] = new SelectList(_context.Customer, "id_customer", "id_customer");
            }
            return Page();
        }

        [BindProperty]
        public Adress Adress { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (User.HasClaim("UserType", "User") || User.HasClaim("UserType", "Seller"))
            {
                Adress.Customer_Id_customer = (int)TempData["customerId"];
            }

            List<Adress> list = await _context.Adress.ToListAsync();
            Adress.id_adress = list.Count() > 0 ? list.Last().id_adress + 1 : 1;

            _context.Adress.Add(Adress);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
