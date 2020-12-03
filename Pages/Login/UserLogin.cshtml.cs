using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ZTPSBD.Data;

namespace ZTPSBD.Pages.Login
{
    public class UserLoginModel : PageModel
    {
        private readonly ZTPSBD.Data.ZTPSBDContext _context;
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public ZTPSBD.Data.User user { get; set; }
        public UserLoginModel(IConfiguration configuration, ZTPSBD.Data.ZTPSBDContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        private bool ValidateUser(ZTPSBD.Data.User user)
        {
            var users = _context.User.ToList();
            User us = users.Find(u => u.login.Equals(user.login));
            if(us == null) { return false; }

            user.User_Type = us.User_Type;
            String Password = us.password;

            if (user.password == String.Empty)
                return false;
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            
            if (passwordHasher.VerifyHashedPassword(user.login, Password, user.password) ==
                PasswordVerificationResult.Success)
                return true;
            else
                return false;

            //return user.password.Equals(Password);
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = "/")
        {

            if (ValidateUser(user))
            {

                var claims = new List<Claim>();

                claims.Add(new Claim("UserType", user.User_Type));

                claims.Add(new Claim(ClaimTypes.Name, user.login));



                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                
                await HttpContext.SignInAsync("CookieAuthentication", new  ClaimsPrincipal(claimsIdentity));
              
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return Redirect("/Index");
            }
            return Page();
        }

        public void OnGet()
        {
        }
    }
}
