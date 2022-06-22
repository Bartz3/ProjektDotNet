using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using Projekt.Infrastructure;

namespace Projekt.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly Projekt.Data.ProjektContext _context;

        public IndexModel(Projekt.Data.ProjektContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get;set; } = default!;

        public async Task<RedirectToPageResult> OnGetAsync()
        {
            if (help.ValidateWorker(HttpContext) == false) return RedirectToPage("/Employees/Index");
            if (_context.Employee != null)
            {
                Employee = await _context.Employee
                .Include(e => e.Firm).ToListAsync();
            }
            return null;    
        }
    }
}
