using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Infrastructure;
using Projekt.Models;

namespace Projekt.Pages.Firms
{
    public class DetailsModel : PageModel
    {
        private readonly Projekt.Data.ProjektContext _context;

        public DetailsModel(Projekt.Data.ProjektContext context)
        {
            _context = context;
        }

      public Firm Firm { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (help.ValidateWorker(HttpContext) == false) return RedirectToPage("./Index");
            if (id == null || _context.Firm == null)
            {
                return NotFound();
            }

            var firm = await _context.Firm.FirstOrDefaultAsync(m => m.Id == id);
            if (firm == null)
            {
                return NotFound();
            }
            else 
            {
                Firm = firm;
            }
            return Page();
        }
    }
}
