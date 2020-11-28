using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
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
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public User user { get; set; }
        public UserLoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private bool ValidateUser(User user)
        {
            string Password = String.Empty;
            string Type = String.Empty;
            string myCompanyDB_connection_string = _configuration.GetConnectionString("MagazynContext");
            SqlConnection con = new SqlConnection(myCompanyDB_connection_string);
            SqlCommand cmd = new SqlCommand("GetUserPSWD", con);
            SqlParameter param = new SqlParameter("@usrname ", user.login);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(param);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Password = reader["password"].ToString();
                Type = reader["User_Type"].ToString();
            }
            user.User_Type = Type;

            reader.Close(); con.Close();

            //if (Password == String.Empty)
            //    return false;
            //PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

            //string hash = passwordHasher.HashPassword(user.login, user.password);


            //if (passwordHasher.VerifyHashedPassword(user.login, hash, user.password) ==
            //    PasswordVerificationResult.Success)
            //    return true;
            //else
            //    return false;

            return user.password.Equals(Password);
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            if (ValidateUser(user))
            {

                var claims = new List<Claim>()
                {
                new Claim("IamUser", user.login)
                };

                if (user.User_Type.Equals("TYPE_OF_ADMIN"))
                    claims.Add(new Claim("IamAdmin", user.login));

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new  ClaimsPrincipal(claimsIdentity));
         
                return LocalRedirect(returnUrl);
            }
            return Page();
        }

        public void OnGet()
        {
        }
    }
}