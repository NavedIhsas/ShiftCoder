using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace _0_FrameWork.Application
{
    public interface IAuthHelper
    {
        void SignOut();
        void Signin(AuthHelperViewModel account);
        bool IsAuthenticated();
        string CurrentAccountFullName();
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
        public string CurrentAccountFullName()
        {
            if (!IsAuthenticated()) return null;
            return _httpContextAccessor.HttpContext.User.Claims.
                FirstOrDefault(x => x.Type == "FullName")?.Value;
        }
        public void Signin(AuthHelperViewModel account)
        {
            var claims = new List<Claim>
            {
                new Claim("FullName",account.Fullname),
                new Claim("AccountId", account.AccountId.ToString()),
                new Claim(ClaimTypes.Name, account.Email),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("Email", account.Email), // Or Use ClaimTypes.NameIdentifier
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public bool IsAuthenticated()
            => _httpContextAccessor.HttpContext.User.Identity is { IsAuthenticated: true };
    }
}
