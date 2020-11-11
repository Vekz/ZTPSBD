using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Product_Order_s_
{
    public class IndexModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public IndexModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public IList<Product_Order> Product_Order { get;set; }

        public async Task OnGetAsync()
        {
            Product_Order = await _context.Product_Order
                .Include(p => p.order)
                .Include(p => p.product).ToListAsync();
        }
    }
}
