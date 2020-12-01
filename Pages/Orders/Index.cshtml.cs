using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            if (User.HasClaim("UserType", "User"))
            {
                string login = String.Empty;
                List<ZTPSBD.Data.User> users = await _context.User.ToListAsync();

                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                login = User.Identity.Name;
                int Id = users.Find(user => user.login.Equals(login)).id_user;

                int customerId = _context.Customer.Where(c => c.User_id_user == Id).SingleOrDefault().id_customer;
                var cuOrders = _context.Customer_Order.Where(co => co.Customer_id_customer == customerId);
                /*var ids = orders.
                Order = _context.Order.SelectMany(o => orders.Contains(o.id_order));
                */
                Order = await _context.Order.Join(cuOrders,
                                            or => or.id_order,
                                            co => co.Order_id_order,
                                            (or, co) => or).ToListAsync();
            }
            else
            {
                Order = await _context.Order.ToListAsync();
            }

            CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                Order = Order.Where(s => s.id_order.Equals(Int32.Parse(searchString))).ToList();
            }
        }
    }
}
