using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public DetailsModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customer
                .Include(c => c.user).FirstOrDefaultAsync(m => m.id_customer == id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
