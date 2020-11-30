using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Payments
{
    public class IndexModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public string CurrentFilter { get; set; }

        public IndexModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        public IList<Payment> Payment { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            CurrentFilter = searchString;

            Payment = await _context.Payment.Include(p => p.order).ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                Payment = Payment.Where(s => s.id_payment.Equals(Int32.Parse(searchString))).ToList();
            }
        }
    }
}
