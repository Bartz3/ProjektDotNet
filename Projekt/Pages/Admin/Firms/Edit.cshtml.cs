using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Infrastructure;
using Projekt.Models;

namespace Projekt.Pages.Firms
{
    public class EditModel : PageModel
    {
        private readonly Projekt.Data.ProjektContext _context;

        public EditModel(Projekt.Data.ProjektContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Firm Firm { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (help.ValidateManager(HttpContext) == false) return RedirectToPage("./Index");
            if (id == null || _context.Firm == null)
            {
                return NotFound();
            }

            var firm =  await _context.Firm.FirstOrDefaultAsync(m => m.Id == id);
            if (firm == null)
            {
                return NotFound();
            }
            Firm = firm;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Firm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FirmExists(Firm.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FirmExists(int id)
        {
          return (_context.Firm?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
