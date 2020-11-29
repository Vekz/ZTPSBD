using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZTPSBD.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ZTPSBD.Pages.PlaceOrder
{
    public class customerPlaceOrderModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;
        [BindProperty]
        public Order Order { get; set; }

        [BindProperty]
        public Product_Order product_order { get; set; }

        [BindProperty]
        public Customer_Order customer_order { get; set; }

        [BindProperty]
        public Payment payment { get; set; }

        public customerPlaceOrderModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }



        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string login = String.Empty;

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;


            foreach (Claim c in claims)
            {
                if(c.Type == "UserName")
                {
                    login = c.Value;
                }
            }


            List< ZTPSBD.Data.User > users = await _context.User.ToListAsync();
            Data.User user = users.Find(user => user.login.Equals(login));
            List<Customer> customers = await _context.Customer.ToListAsync();
            int customer_ID = customers.Find(customer => customer.User_id_user == user.id_user).id_customer;              

            customer_order.Customer_id_customer = customer_ID;
            customer_order.Customer_id_customer = Order.id_order;

            _context.Order.Add(Order);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
