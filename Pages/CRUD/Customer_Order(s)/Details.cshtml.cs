using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Customer_Order_s_
{
    public class DetailsModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public DetailsModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public Customer_Order Customer_Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer_Order = await _context.Customer_Order
                .Include(c => c.customer)
                .Include(c => c.order).FirstOrDefaultAsync(m => m.Customer_id_customer == id);

            if (Customer_Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
