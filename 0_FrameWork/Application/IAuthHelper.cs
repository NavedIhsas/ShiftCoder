using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace _0_FrameWork.Application
{
    public interface IAuthHelper
    {
        void SignOut();
        void Signin(AuthHelperViewModel account);
    }

    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;//add to startup

        public AuthHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SignOut()
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public void Signin(AuthHelperViewModel account)
        {
            var claim = new List<Claim>()
            {
                new Claim("AccountId",account.AccountId.ToString()),
                new Claim(ClaimTypes.Role,account.RoleId.ToString()),
                new Claim(ClaimTypes.Email,account.Email),
                new Claim(ClaimTypes.Name,account.Fullname)
            };
            var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
            };

            _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), properties);


        }
    }
}
