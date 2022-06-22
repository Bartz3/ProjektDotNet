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
    public class DetailsModel : PageModel
    {
        private readonly Projekt.Data.ProjektContext _context;

        public DetailsModel(Projekt.Data.ProjektContext context)
        {
            _context = context;
        }

      public Employee Employee { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (help.ValidateManager(HttpContext) == false) return RedirectToPage("./Index");
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else 
            {
                Employee = employee;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
        
            var cookie = Request.Cookies["superPracownik"];
            if (cookie == null)
            {
                cookie = String.Empty;
            }
            cookie += "," + id.ToString();
            //var employee = await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);
            //_context.Employee.Add(employee);

            Response.Cookies.Append("superPracownik", cookie);

           // await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/top");
        }
    }
}
