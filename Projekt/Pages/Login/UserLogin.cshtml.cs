using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Projekt.Models;
using Projekt.DAL;
using System.Security.Claims;

namespace Projekt.Pages.Login
{

    public class UserLoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        IEmployeeDB employeeDB;
        public string Message { get; set; }
        [TempData]
        public string userNick { get; set; }
        [BindProperty]
        
        public Employee user { get; set; }
        public UserLoginModel(IConfiguration configuration,IEmployeeDB _employeeDB)
        {
            _configuration = configuration;
            employeeDB= _employeeDB;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Nick") != null)
            {
                return RedirectToPage("/Index");
            }
            return null;
        }

        private bool ValidateUser(Employee UserToValidate)
        {
            //if ((user.userName == "admin") && (user.password == "abc"))
            //    return true;
            //else
            //    return false;

            List<Employee> siteUsers;

            siteUsers = employeeDB.List();

            foreach (var User in siteUsers)
            {

                string log = String.Concat(User.userName.Where(c => !Char.IsWhiteSpace(c)));
                string psswd = String.Concat(User.password.Where(c => !Char.IsWhiteSpace(c)));
                var hash = SecurePasswordHasher.Hash(psswd);
                var result = SecurePasswordHasher.Verify(psswd, hash);

                if ((UserToValidate.userName == log) && result)
                {
                    HttpContext.Session.SetString("Nick", UserToValidate.userName);
                    userNick = UserToValidate.userName;
                    return true;

                }
            }
            return false;

        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ValidateUser(user))
            {
                var claims = new List<Claim>()
                {
                new Claim(ClaimTypes.Name, user.userName)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new
               ClaimsPrincipal(claimsIdentity));
                return RedirectToPage("/Index");
            }
            ViewData["Message"] = "Niepoprawne dane";
            return Page();
        }
    }

}
