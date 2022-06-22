using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.Data;
using Projekt.Infrastructure;
using Projekt.Models;

namespace Projekt.Pages.Firms
{
    public class CreateModel : PageModel
    {
        private readonly Projekt.Data.ProjektContext _context;

        public CreateModel(Projekt.Data.ProjektContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (help.ValidateManager(HttpContext) == false) return RedirectToPage("./Index");
            return Page();
        }

        [BindProperty]
        public Firm Firm { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Firm == null || Firm == null)
            {
                return Page();
            }

            _context.Firm.Add(Firm);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
