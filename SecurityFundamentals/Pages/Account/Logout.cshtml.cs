using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecurityFundamentals.Pages.Account
{
    public class LogoutModel : PageModel
    {
        // Need to create a partial view somewhere that can post here
        public async Task<IActionResult> OnPostAsync()
        {            
            await HttpContext.SignOutAsync("GaborsAuthCookie");

            return RedirectToPage("/Index");
        }
    }
}
