using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Projekt.Models;
using System.Security.Claims;

namespace Projekt.Pages.Login
{

    public class UserLoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [TempData]
        public string userNick { get; set; }
        [BindProperty]
        
        public Employee user { get; set; }
        public UserLoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
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

            List<Employee> siteUsers = new List<Employee>();

            string myCompanyDBcs = _configuration.GetConnectionString("ProjektContext");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM Employee";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Employee _user;

            while (reader.Read())
            {
                _user = new Employee();
                _user.Id = int.Parse(reader["Id"].ToString());
                _user.userName = reader["userName"].ToString();
                _user.password = reader["password"].ToString();

                siteUsers.Add(_user);
            }
            reader.Close(); con.Close();

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
