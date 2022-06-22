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
    public class IndexModel : PageModel
    {
        private readonly Projekt.Data.ProjektContext _context;

        public IndexModel(Projekt.Data.ProjektContext context)
        {
            _context = context;
        }

        public IList<Firm> Firm { get;set; } = default!;

        public async Task<RedirectToPageResult> OnGetAsync()
        {
            if (help.ValidateWorker(HttpContext) == false) return RedirectToPage("./Index");
            if (_context.Firm != null)
            {
                Firm = await _context.Firm.ToListAsync();
            }
            return null;
        }
    }
}
