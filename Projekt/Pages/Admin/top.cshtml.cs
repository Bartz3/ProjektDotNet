using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.Models;
using Projekt.DAL;

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
            superEmployees = _employeeDB.List();
            //Response.Cookies.Delete("superPracownik");
            var cookie = Request.Cookies["superPracownik"];

            if (cookie == null)
            {
                return;
            }
            string[] IDs = cookie.Split(',');
            int pom;
            Employee employee;
            foreach (var id in IDs)
            {
                bool bool2 = int.TryParse(id, out pom);
                if (!bool2)
                    continue;
                employee =  _context.Employee.FirstOrDefault(m => m.Id == pom);
                superEmployees.Add(employee);

                //if (employee.firstName!=null || employee.lastName!=null)
                    
            }
        }
    }
}
