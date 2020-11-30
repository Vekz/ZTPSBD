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

        public string NameSort { get; set; }
        public string PriceSort { get; set; }
        public string DateSort { get; set; }
        public string MassSort { get; set; }
        public string CurrentFilter { get; set; }

        [BindProperty]
        public int Id { get; set; }

        public IndexModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            PriceSort = sortOrder == "Price" ? "price_desc" : "Price";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            MassSort = sortOrder == "Mass" ? "mass_desc" : "Price";

            CurrentFilter = searchString;

            Product = await _context.Product.Include(p => p.product_Category).ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                Product = Product.Where(s => s.name.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    Product = Product.OrderByDescending(s => s.name).ToList();
                    break;
                case "Date":
                    Product = Product.OrderBy(s => s.expiration_date).ToList();
                    break;
                case "date_desc":
                    Product = Product.OrderByDescending(s => s.expiration_date).ToList();
                    break;
                case "Price":
                    Product = Product.OrderBy(s => s.price).ToList();
                    break;
                case "price_desc":
                    Product = Product.OrderByDescending(s => s.price).ToList();
                    break;
                case "Mass":
                    Product = Product.OrderBy(s => s.mass).ToList();
                    break;
                case "mass_desc":
                    Product = Product.OrderByDescending(s => s.mass).ToList();
                    break;
                default:
                    Product = Product.OrderBy(s => s.name).ToList();
                    break;
            }

        }



        public async Task OnPostAdd()
        {
            string cookieResp = Id.ToString() + ',';
            if (!(Request.Cookies["ShCart"] == null))
            {
                cookieResp += Request.Cookies["ShCart"];
            }
            Response.Cookies.Append("ShCart", cookieResp, new CookieOptions() { IsEssential = true });

            await OnGetAsync("", "");
        }

    }
}
