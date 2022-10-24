using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WegisterUI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWegisterDbContextFactory _wegisterDbContextFactory;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger, UserManager<IdentityUser> userManager, IWegisterDbContextFactory wegisterDbContextFactory)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _wegisterDbContextFactory = wegisterDbContextFactory;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            var user = await _userManager.GetUserAsync(User);
            var context = _wegisterDbContextFactory.CreateDbContext();

            var companyId = context.Users.Single(u => u.Id == new Guid(user.Id)).CompanyId;

            var result = await _userManager.RemoveClaimsAsync(user, 
                new List<Claim>
                {
                    new ("companyId", companyId)
                });

            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
