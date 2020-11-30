using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Orders
{
    public class IndexModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public string CurrentFilter { get; set; }

        public IndexModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            CurrentFilter = searchString;

            Order = await _context.Order.ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                Order = Order.Where(s => s.id_order.Equals(Int32.Parse(searchString))).ToList();
            }
        }
    }
}
