using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Customers
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
        public Customer Customer { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Customer.User_id_user = (int)TempData["userId"];

            List<Customer> list = await _context.Customer.ToListAsync();
            Customer.id_customer = list.Count() > 0 ? list.Last().id_customer + 1 : 1;

        
            _context.Customer.Add(Customer);
            await _context.SaveChangesAsync();

            if (!User.HasClaim("UserType", "Admin")) { return RedirectToPage("/Login/UserLogin"); }

            return RedirectToPage("./Index");
        }
    }
}
