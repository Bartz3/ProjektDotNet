using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Projekt.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Projekt.DAL;

namespace Projekt.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;
        IEmployeeDB employeeDB;

        [TempData]
        public string newAccount { get; set; }
        [BindProperty]
        public Employee user { get; set; }

        [BindProperty]
        [Display(Name = "Powtórz has³o"),DataType(DataType.Password)]
        public string passwd2 { get; set; }
    
        public RegisterModel(IConfiguration configuration,IEmployeeDB _employeeDB)
        {
            _configuration = configuration;
            employeeDB = _employeeDB;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Nick") != null)
            {
                return RedirectToPage("/Index");
            }
            return null;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (user.password == passwd2)
            {
                employeeDB.addNewUser(user);

                newAccount = "Gratulacje " +user.userName +" za³o¿y³eœ swoje konto na stronie!";

                return RedirectToPage("/Index");
            }
            else return Page();
        }
    }
}
