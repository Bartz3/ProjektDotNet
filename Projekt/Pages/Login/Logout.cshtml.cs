using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projekt.Pages.Login
{
    public class LogoutModel : PageModel
    {
 
            public async Task<IActionResult> OnGet()
            {
                await HttpContext.SignOutAsync("CookieAuthentication");
                HttpContext.Session.Remove("Nick");
                return this.RedirectToPage("/Index");
            }
        
    }
}
