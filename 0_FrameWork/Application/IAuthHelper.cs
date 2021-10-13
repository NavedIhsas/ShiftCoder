using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace _0_FrameWork.Application
{
    public interface IAuthHelper
    {
        void SignOut();
        void Signin(AuthHelperViewModel account);
        bool IsAuthenticated();
        string CurrentAccountFullName();
        List<int> GetPermissions();
    }

    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor; //add to startup

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
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "FullName")?.Value;
        }

        public List<int> GetPermissions()
        {
            if (!IsAuthenticated()) return new List<int>();

            var permissions = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Permissions")
                ?.Value;

            return JsonConvert.DeserializeObject<List<int>>(permissions); //convert form string to List<int>
        }

        public void Signin(AuthHelperViewModel account)
        {
            var permission = JsonConvert.SerializeObject(account.Permissions); //convert to string
            var claims = new List<Claim>
            {
                new("FullName", account.Fullname),
                new("AccountId", account.AccountId.ToString()),
                new(ClaimTypes.Name, account.Email),
                new(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                new(ClaimTypes.Role, account.RoleId.ToString()),
                new("Email", account.Email), // Or Use ClaimTypes.NameIdentifier
                new("Permissions", permission) 
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = account.RememberMe
            };
            _httpContextAccessor.HttpContext.SignInAsync(principal, properties);
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity is { IsAuthenticated: true };
        }
    }
}