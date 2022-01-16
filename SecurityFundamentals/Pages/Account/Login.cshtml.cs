using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SecurityFundamentals.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // We don't have a data store so we are hard-coding the verification
                // 1. Verify Credentials
                if (Credential.UserName == "admin" && Credential.Password == "asdf")
                {
                    // 2. Create Security Context

                    var claims = new List<Claim>();

                    var claim1 = new Claim(ClaimTypes.Name, "admin");
                    var claim2 = new Claim(ClaimTypes.Email, "admin@website.com");

                    claims.Add(claim1);
                    claims.Add(claim2);

                    var identity = new ClaimsIdentity(claims, "GaborsAuthCookie");

                    var principal = new ClaimsPrincipal(identity);

                    // Take ClaimsPrincipal
                    // Serialize it
                    // Encrypt it
                    // Put it in a Cookie
                    await HttpContext.SignInAsync("GaborsAuthCookie", principal);

                    return RedirectToPage("/Index");
                }
            }

            return Page();
        }
    }

    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
