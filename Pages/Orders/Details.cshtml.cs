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
    public class DetailsModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public DetailsModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }
        public Dictionary<Product, int> Products = new Dictionary<Product, int>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Order.FirstOrDefaultAsync(m => m.id_order == id);

            if (Order == null)
            {
                return NotFound();
            }

            var prodOrders = _context.Product_Order.Where(po => po.Order_id_order == Order.id_order);
            var prods = await _context.Product.Join(prodOrders, pr => pr.id_product, po => po.Product_id_product, (pr, po) => pr).Include(p => p.product_Category).ToListAsync();

            var ord = prodOrders.ToList();
            for(int i=0; i<prods.Count; i++)
            {
                Products[prods[i]] = ord.ElementAt(i).Count;
            }
            return Page();
        }
    }
}
