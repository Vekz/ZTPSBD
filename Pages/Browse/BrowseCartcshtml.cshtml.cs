using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZTPSBD.DAL;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.Browse
{
    public class BrowseCartcshtmlModel : PageModel
    {

        public CartDB cartDB;

        public ZTPSBDContext _context;


        public string jsonCartDB { get; set; }
       
        
        public BrowseCartcshtmlModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
            cartDB = new CartDB();
        }

        public async Task OnGet(int id)
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


        public IActionResult OnPostDelete()
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

                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(-5)
                    };

                    Response.Cookies.Append(cartDB.GetProductList.Count.ToString(), "-1", cookieOptions);
                    Response.Cookies.Delete(add);
                }
            }

            return Page();
        }
        
        public IActionResult OnPostRemove(int id)
        {
            for (int i = 1; ; i++)
            {
                string add = Request.Cookies[i.ToString()];
                if (add == null)
                {
                    break;
                }
                else
                {
                    if (int.Parse(add) == id)
                    {
                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(-5)
                        };
                        Response.Cookies.Append(cartDB.GetProductList.Count.ToString(), "-1", cookieOptions);
                        Response.Cookies.Delete(add);
                    }
                }
            }

            return Page();


        }     

        public IActionResult OnPostPlace()
        {

            return RedirectToPage("/PlaceOrder/customerPlaceOrder");
        }




    }
}
