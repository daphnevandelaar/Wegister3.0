using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WegisterUI.Areas.Identity
{
    public class SignInManagerOverride<TUser> : SignInManager<TUser> where TUser : class
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SignInManagerOverride(UserManager<TUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<TUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<TUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<TUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _contextAccessor = contextAccessor;
        }

        public void AddCompanyToClaim(string companyId)
        {
            var claimIdentity = new ClaimsIdentity();
            claimIdentity.AddClaim(new Claim("CompanyId", "999119f9-ed3c-41b3-994b-96d666cf0d7c"));
            _contextAccessor?.HttpContext?.User.AddIdentity(claimIdentity);
        }
    }
}
