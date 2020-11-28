using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;
using ZTPSBD.DAL;
using Microsoft.AspNetCore.Http;

namespace ZTPSBD.Pages.CRUD.Products
{
    public class IndexModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public CartDB cartDB;



        public string jsonCartDB { get; set; }

        public IndexModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
            cartDB = new CartDB();
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Product.Include(p => p.product_Category).ToListAsync();
            populatecartDB();
        }



        public IActionResult OnPostAdd(int id)
        {
            populatecartDB();

            Product product = _context.Product.Find(id);

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            };
            cartDB.AddToCart(product);

            Response.Cookies.Append(cartDB.GetProductList.Count.ToString(), product.id_product.ToString(), cookieOptions);

         
            return Redirect("/Browse/Products");
        }


        private async void populatecartDB()
        {
            cartDB.Clear();
            for (int i = 1; ; i++)
            {
                string add = Request.Cookies[i.ToString()];
                if (add == null)
                {
                    break;
                }
                else
                {
                    Product productToAdd = await _context.Product.FindAsync(int.Parse(add));
                    cartDB.AddToCart(productToAdd);
                }

            }
        }

    }
}
