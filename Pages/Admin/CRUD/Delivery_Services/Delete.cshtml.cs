using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.CRUD.Delivery_Services
{
    public class DeleteModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;

        public DeleteModel(ZTPSBD.Data.ZTPSBDContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Delivery_Service Delivery_Service { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Delivery_Service = await _context.Delivery_Service
                .Include(d => d.order).FirstOrDefaultAsync(m => m.id_deliverman == id);

            if (Delivery_Service == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Delivery_Service = await _context.Delivery_Service.FindAsync(id);

            if (Delivery_Service != null)
            {
                _context.Delivery_Service.Remove(Delivery_Service);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
