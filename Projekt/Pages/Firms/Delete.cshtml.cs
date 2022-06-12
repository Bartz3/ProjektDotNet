using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Firms
{
    public class DeleteModel : PageModel
    {
        private readonly Projekt.Data.ProjectContext _context;

        public DeleteModel(Projekt.Data.ProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Firm Firm { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Firm == null)
            {
                return NotFound();
            }
            var firm = await _context.Firm.FindAsync(id);

            if (firm != null)
            {
                Firm = firm;
                _context.Firm.Remove(Firm);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
