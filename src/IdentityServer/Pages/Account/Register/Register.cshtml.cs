using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using IdentityServer.Models;
using IdentityServer.Pages.Account.Register;
using System.Security.Claims;
using Duende.IdentityModel;

namespace IdentityServer.Pages.Register
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private UserManager<ApplicationUser> _userManager;
        public RegisterModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public RegisterViewModel? Input { get; set; }
        [BindProperty]
        public bool RegisterSuccess { get; set;}

        public IActionResult OnGet(string returnUrl)
        {
            Input = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (Input.Button != "register")
            {
                return Redirect("~/");
            }
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = Input.Login,
                    Email = Input.Login,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(applicationUser, Input.Password!);

                if (result.Succeeded)
                {
                    await _userManager.AddClaimsAsync(applicationUser, new Claim[]{
                        new Claim(JwtClaimTypes.Name,Input.Login!)
                    });
                    RegisterSuccess = true;
                }
            }
            return Page();
        }
    }
}
