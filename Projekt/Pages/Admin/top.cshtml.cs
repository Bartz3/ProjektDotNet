using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.Models;
using Projekt.DAL;
using Projekt.Infrastructure;
namespace Projekt.Pages.Admin
{
    public class topModel : PageModel
    {
        private readonly Projekt.Data.ProjektContext _context;
        IEmployeeDB _employeeDB;
        private readonly IConfiguration _configuration;
        
        public List<Employee> superEmployees = new List<Employee>();

        public Employee Employee { get; set; } = default!;

        public topModel(IConfiguration configuration,IEmployeeDB employeeDB,Projekt.Data.ProjektContext context)
        {
           
            _employeeDB = employeeDB;
            _configuration = configuration;
            _context = context;
        }

        public void OnGet()
        {
            help.ValidateWorker(HttpContext);
             var cookie = Request.Cookies["superPracownik"];

            if (cookie == null)
            {
                return;
            }
            string[] IDs = cookie.Split(',');
            int pom;
            Employee employee,pom2;
            foreach (var id in IDs)
            {
                 bool isInList=false;
                bool bool2 = int.TryParse(id, out pom);
                if (!bool2)
                    continue;
                employee = _context.Employee.FirstOrDefault(m => m.Id == pom);

                if(employee != null)
                         superEmployees.Add(employee);
            }
        }

        public void OnPost()
        {
            Response.Cookies.Delete("superPracownik");

            //return RedirectToPage("/Index");
        }
    }
}
