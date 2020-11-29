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
    public class IndexModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public IndexModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public IList<Customer_Order> Customer_Order { get;set; }

        public async Task OnGetAsync()
        {
            Customer_Order = await _context.Customer_Order
                .Include(c => c.customer)
                .Include(c => c.order).ToListAsync();
        }
    }
}
