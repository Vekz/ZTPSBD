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

        public Dictionary<int, int> ShoppingCart = new Dictionary<int, int>();

        [BindProperty]
        public Order Order { get; set; }


        [BindProperty]
        public Customer_Order customer_order { get; set; }

        [BindProperty]
        public Payment payment { get; set; }

        [BindProperty]
        public Delivery_Service delivery_service { get; set; }

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

            //Fields init
            string login = String.Empty;
            List<Order> orders = await _context.Order.ToListAsync();
            List<Customer> customers = await _context.Customer.ToListAsync();
            List<ZTPSBD.Data.User> users = await _context.User.ToListAsync();
            List<Payment> payments = await _context.Payment.ToListAsync();
            List<Delivery_Service> delivery_Services = await _context.Delivery_Service.ToListAsync();

            #region get Customer ID - I wish i could make it better ( I can but i wont atm)
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            foreach (Claim c in claims)
            {
                if(c.Type == "UserName")
                {
                    login = c.Value;
                }
            }
            Data.User user = users.Find(user => user.login.Equals(login));
            int customer_ID = customers.Find(customer => customer.User_id_user == user.id_user).id_customer;
            #endregion

            #region setting order fields
            //Order ID
            Order.id_order = orders.Count > 0 ? orders.Last().id_order + 1 : 1;
            //Payment Fields
            payment.id_payment = payments.Count > 0 ? payments.Last().id_payment + 1 : 1;
            payment.Order_id_order = Order.id_order;
            payment.due_date = Order.due_date.AddDays(3d);
            //Payment ID
            Order.Payment_id_payment = payment.id_payment;
            //Delivery Service Fields
            delivery_service.id_deliverman = delivery_Services.Count > 0 ? delivery_Services.Last().id_deliverman + 1 : 1 ;
            delivery_service.Order_id_order = Order.id_order;
            delivery_service.delivery_date = Order.due_date;
            Order.Delivery_Service_id_deliveryman = delivery_service.id_deliverman;
            #endregion

            //the stupid tables which i hate
            customer_order.Customer_id_customer = customer_ID;
            customer_order.Customer_id_customer = Order.id_order;

          //  List<Product_Order> product_Orders_temp = new List<Product_Order>();
            
            foreach(KeyValuePair<int,int> pair in ShoppingCart)
            {
                _context.Product_Order.Add(new Product_Order() { Order_id_order = Order.id_order, Product_id_product = pair.Key });
                await _context.SaveChangesAsync();
            }

            //add everything do database
            _context.Add(delivery_service);
            await _context.SaveChangesAsync();
            _context.Add(customer_order);
            await _context.SaveChangesAsync();
            _context.Add(payment);
            await _context.SaveChangesAsync();
            _context.Order.Add(Order);
            await _context.SaveChangesAsync();;


            return RedirectToPage("./Index");
        }

       
        public void parseCookie()
        {
            //Stolne code from artur
            ShoppingCart.Clear();
            if (Request.Cookies["ShCart"] != null)
            {
                List<string> cookieContent = new List<string>(Request.Cookies["ShCart"].Split(","));
                cookieContent.Remove("");

                foreach (string n in cookieContent)
                {
                    int p = System.Convert.ToInt32(n);
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
}
