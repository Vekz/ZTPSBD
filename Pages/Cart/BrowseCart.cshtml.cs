using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.Browse
{
    public class BrowseCartcshtmlModel : PageModel
    {

        public Dictionary<Product, int> ShoppingCart = new Dictionary<Product, int>();
        [BindProperty]
        public int Id { get; set; }

        public ZTPSBDContext _context;


        public BrowseCartcshtmlModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            ShoppingCart.Clear();
            if (Request.Cookies["ShCart"] != null)
            {
                List<Product> productList;
                List<string> cookieContent = new List<string>(Request.Cookies["ShCart"].Split(","));
                cookieContent.Remove("");
                productList = await _context.Product.Include(p => p.product_Category).ToListAsync();
                foreach (Product p in productList)
                {
                    foreach (string n in cookieContent)
                    {
                        if (System.Convert.ToInt32(n) == p.id_product)
                        {
                            if (!ShoppingCart.TryGetValue(p, out int val))
                            {
                                ShoppingCart.Add(p, val);
                                ShoppingCart[p] += 1;
                            }
                            else
                            {
                                ShoppingCart[p] += 1;
                            }
                        }
                    }
                }
            }
        }


        public IActionResult OnPostClear()
        {
            ShoppingCart.Clear();
            Response.Cookies.Append("ShCart", "", new CookieOptions() { IsEssential = true });
            return Page();
        }
        
        public async Task OnPostRemove()
        {
            string cookie = Request.Cookies["ShCart"];

            List<string> cookieContent = new List<string>(Request.Cookies["ShCart"].Split(","));
            cookieContent.Remove("");
            Response.Cookies.Append("ShCart", "", new CookieOptions() { IsEssential = true });
            foreach (string n in cookieContent)
            {
                if (Int32.Parse(n) != Id)
                {
                    string cookieResp = n.ToString() + ',';
                    Response.Cookies.Append("ShCart", cookieResp, new CookieOptions() { IsEssential = true });
                }   
            }

            await OnGetAsync();
        }   
        
        
        public IActionResult OnPostPlace()
        {
            TempData["BuyNow"] = Request.Cookies["ShCart"];
            ShoppingCart.Clear();
            Response.Cookies.Append("ShCart", "", new CookieOptions() { IsEssential = true });
            return RedirectToPage("/PlaceOrder/customerPlaceOrder");
        }
    }
}
