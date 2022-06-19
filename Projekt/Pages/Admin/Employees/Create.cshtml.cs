using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly Projekt.Data.ProjektContext _context;

        public List<Roles>roles=new List<Roles>();

        public CreateModel(Projekt.Data.ProjektContext context)
        {
            _context = context;

            roles.Add(Roles.Admin);
            roles.Add(Roles.Manager);
            roles.Add(Roles.Worker);
        }

        public IActionResult OnGet()
        {
            ViewData["FirmID"] = new SelectList(_context.Set<Firm>(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Employee == null || Employee == null)
            {
                return Page();
            }

            _context.Employee.Add(Employee);
            
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
