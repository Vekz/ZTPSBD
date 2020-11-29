using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;
using Microsoft.AspNetCore.Http;

namespace ZTPSBD.Pages.CRUD.Products
{
    public class IndexModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        [BindProperty]
        public int Id { get; set; }

        public IndexModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Product.Include(p => p.product_Category).ToListAsync();
        }



        public async Task OnPostAdd()
        {
            string cookieResp = Id.ToString() + ',';
            if (!(Request.Cookies["ShCart"] == null))
            {
                cookieResp += Request.Cookies["ShCart"];
            }
            Response.Cookies.Append("ShCart", cookieResp, new CookieOptions() { IsEssential = true });

            await OnGetAsync();
        }

    }
}
