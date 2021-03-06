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
            Credential = new Credential();
            Credential.UserName = "admin";
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
                    var claim3 = new Claim("Department", "HR");
                    var claim4 = new Claim("Admin", "true");
                    var claim5 = new Claim("Manager", "true");
                    var claim6 = new Claim("EmploymentDate", "2021-12-01");

                    claims.Add(claim1);
                    claims.Add(claim2);
                    claims.Add(claim3);
                    claims.Add(claim4);
                    claims.Add(claim5);
                    claims.Add(claim6);

                    var identity = new ClaimsIdentity(claims, "GaborsAuthCookie");

                    var principal = new ClaimsPrincipal(identity);

                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = Credential.RememberMe;

                    // Take ClaimsPrincipal
                    // Serialize it
                    // Encrypt it
                    // Put it in a Cookie
                    await HttpContext.SignInAsync("GaborsAuthCookie", principal, authProperties);

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

        [Required]
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }

}
