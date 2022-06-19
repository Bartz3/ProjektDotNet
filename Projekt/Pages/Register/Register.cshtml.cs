using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Projekt.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Projekt.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [TempData]
        public string newAccount { get; set; }
        [BindProperty]
        public Employee user { get; set; }
        [BindProperty]
        [Display(Name = "Powtórz has³o"),DataType(DataType.Password)]
        public string passwd2 { get; set; }

        public RegisterModel(IConfiguration configuration)
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (user.password == passwd2)
            {
                var hash = SecurePasswordHasher.Hash(user.password);
                string zad10cs = _configuration.GetConnectionString("ProjektContext");

                SqlConnection con = new SqlConnection(zad10cs);
                SqlCommand cmd = new SqlCommand("sp_userAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter name_SqlParam = new SqlParameter("@userName", SqlDbType.VarChar,
                100);
                name_SqlParam.Value = user.userName;
                cmd.Parameters.Add(name_SqlParam);

                SqlParameter password_SqlParam = new SqlParameter("@password", SqlDbType.VarChar,
                100);
                password_SqlParam.Value = hash;
                cmd.Parameters.Add(password_SqlParam);

                SqlParameter productID_SqlParam = new SqlParameter("@Id",
                SqlDbType.Int);
                productID_SqlParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(productID_SqlParam);
                con.Open();
                int numAff = cmd.ExecuteNonQuery();
                con.Close();

                newAccount = "Gratulacje " +user.userName +" za³o¿y³eœ swoje konto na stronie!";

                return RedirectToPage("/Index");
            }
            else return Page();
        }
    }
}
