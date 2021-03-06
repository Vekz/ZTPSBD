﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZTPSBD.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ZTPSBD.Pages.PlaceOrder
{
    public class customerPlaceOrderModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        //It was supposed to be <product_key, product_count> but product count is not used to anything rn
        public Dictionary<int, int> ShoppingCart = new Dictionary<int, int>();

        [BindProperty]
        public Order Order { get; set; }


        [BindProperty]
        public Customer_Order customer_order { get; set; }

        [BindProperty]
        public Payment payment { get; set; }

        [BindProperty]
        public Delivery_Service delivery_service { get; set; }



        #region Private Fields
        private List<Adress> adresses;

        private string[] paymethods = { "cash", "card" };

        int customer_ID = 0;

        string login = String.Empty;

        public int adress_id = -1;

        List<ZTPSBD.Data.User> users;

        List<Customer> customers; 

        #endregion
        public customerPlaceOrderModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }
        public async Task < IActionResult > OnGet()
        {
            users = await _context.User.ToListAsync();
            customers = await _context.Customer.ToListAsync();

            #region get Customer ID - I wish i could make it better ( I can but i wont atm)


            if (User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                login = User.Identity.Name;
                Data.User user = users.Find(user => user.login.Equals(login));
                customer_ID = customers.Find(customer => customer.User_id_user == user.id_user).id_customer;

            }
            #endregion

            var descriptions =  _context.Adress.Where(a => a.Customer_Id_customer == customer_ID).Select(a => new { id_adress = a.id_adress, Description = string.Format("City:{0} Street:{1} {2}", a.city, a.street, a.street_no)}).ToList();
            adresses = await _context.Adress.Where(a => a.Customer_Id_customer == customer_ID).ToListAsync();

            ViewData["adresses"] = new SelectList(descriptions, "id_adress", "Description");
            ViewData["methods"] = new SelectList(paymethods);

            return Page();
        }

        public async Task<IActionResult> OnPostPlaceAsync()
        {
            users = await _context.User.ToListAsync();
            customers = await _context.Customer.ToListAsync();

            #region get Customer ID - I wish i could make it better ( I can but i wont atm)


            if (User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                login = User.Identity.Name;
                Data.User user = users.Find(user => user.login.Equals(login));
                customer_ID = customers.Find(customer => customer.User_id_user == user.id_user).id_customer;

            }
            #endregion
            var descriptions = _context.Adress.Where(a => a.Customer_Id_customer == customer_ID).Select(a => new { id_adress = a.id_adress, Description = string.Format("City:{0} Street:{1} {2}", a.city, a.street, a.street_no) }).ToList();

            adresses = await _context.Adress.Where(a => a.Customer_Id_customer == customer_ID).ToListAsync();

            if (!ModelState.IsValid)
            {
                ViewData["adresses"] = new SelectList(adresses, "descriptions", "Description");

                return Page();
            }

            //Fields init
            List<Order> orders = await _context.Order.AsNoTracking().ToListAsync();
            List<Payment> payments = await _context.Payment.ToListAsync();
            List<Delivery_Service> delivery_Services = await _context.Delivery_Service.ToListAsync();


            #region setting row fields
            //Order ID
            Order.id_order = orders.Count > 0 ? orders.Last().id_order + 1 : 1;
            StringBuilder stringBuilder = new StringBuilder();
            Adress temp = adresses.Find(a => a.Customer_Id_customer == customer_ID);
            stringBuilder.AppendJoin(':', temp.city, temp.street, temp.street_no.ToString());
            Order.orderAddress = stringBuilder.ToString();

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

            //Delivery ID
            Order.Delivery_Service_id_deliveryman = delivery_service.id_deliverman;

            #endregion

            //Please do not touch the order in which rows are added
            _context.Order.Add(Order);
            await _context.SaveChangesAsync();
            _context.Add(delivery_service);
            await _context.SaveChangesAsync();


            //the stupid tables which i hate
            customer_order.Customer_id_customer = customer_ID;
            customer_order.Order_id_order = Order.id_order;

            _context.Add(payment);
            await _context.SaveChangesAsync();

            _context.Add(customer_order);
            await _context.SaveChangesAsync();

            //Parse Shopping cart cookie
            parseProducts();

            //Add one product Order for each prodcut type, sice DB architecture wont allow for quantity
            foreach (KeyValuePair<int,int> pair in ShoppingCart)
            {
                _context.Product_Order.Add(new Product_Order() {Order_id_order = Order.id_order, Product_id_product = pair.Key, Count = pair.Value });
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }


        private void parseProducts()
        {
            //Stolne code from artur
            ShoppingCart.Clear();
            if (TempData.Peek("BuyNow").ToString() != "")
            {
                List<string> cookieContent = new List<string>(((string)TempData["BuyNow"]).Split(","));
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
