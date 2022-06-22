using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using Projekt.Infrastructure;

namespace Projekt.Pages.Employees
{
    public class RoleModel : PageModel
    {
        private readonly ProjektContext _context;
        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public List<Roles> roles = new List<Roles>();
        public RoleModel(Projekt.Data.ProjektContext context)
        {
            _context = context;
            roles.Add(Roles.Admin);
            roles.Add(Roles.Manager);
            roles.Add(Roles.Worker);

        }
        

      public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (help.ValidateAdmin(HttpContext) == false) return RedirectToPage("./Index");

            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }


            var employee =  await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            Employee = employee;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Employee = await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);
            string pom = Request.Form["Employee.role"].ToString();

            Employee.role = Employee.checkRole(pom);

           // var entity = _context.Employee.Attach(Employee);
           // entity.Entry(Employee).State = EntityState.Modified;

            _context.Attach(Employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.Id))
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



        private bool EmployeeExists(int id)
        {
            return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
