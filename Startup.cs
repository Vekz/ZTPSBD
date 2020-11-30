using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ZTPSBD.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ZTPSBD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<ZTPSBDContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MagazynContext")));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("UserType", "Admin"));
                options.AddPolicy("SellerSuffice", policy => policy.RequireClaim("UserType", "Admin", "Seller"));
                options.AddPolicy("UserSuffice", policy => policy.RequireClaim("UserType", "Admin", "Seller", "User"));
            });

            services.AddAuthentication("CookieAuthentication")
            .AddCookie("CookieAuthentication", config =>
            {
                config.Cookie.HttpOnly = true;
                config.Cookie.SecurePolicy = CookieSecurePolicy.None;
                config.Cookie.Name = "UserLoginCookie";
                config.LoginPath = "/Login/UserLogin";
                config.Cookie.SameSite = SameSiteMode.Strict;
            });

            services.AddRazorPages(options => {
                //Anonymous access
                options.Conventions.AllowAnonymousToPage("/Products/Products");
                options.Conventions.AllowAnonymousToPage("/Products/Details");
                options.Conventions.AllowAnonymousToPage("/Login/UserLogin");
                options.Conventions.AllowAnonymousToFolder("/Cart/");
                options.Conventions.AllowAnonymousToFolder("/PlaceOrder/");

                //User access
                options.Conventions.AuthorizeFolder("/UsersPanel/", "UserSuffice");
                options.Conventions.AuthorizeFolder("/Adresses/", "UserSuffice"); //Only sees his addresses and can only edit them
                options.Conventions.AuthorizePage("/Login/UserLogout", "UserSuffice");
                options.Conventions.AuthorizePage("/Orders/Index", "UserSuffice"); //But only his
                options.Conventions.AuthorizePage("/Orders/Details", "UserSuffice"); //But only his
                options.Conventions.AuthorizePage("/Orders/Delete", "UserSuffice"); //But only his
                options.Conventions.AuthorizePage("/Users/Edit", "UserSuffice"); //But only his

                //Seller access
                options.Conventions.AuthorizeFolder("/SellersPanel/", "SellerSuffice");
                options.Conventions.AuthorizeFolder("/Product_Categories/", "SellerSuffice");
                options.Conventions.AuthorizeFolder("/Product_Order(s)/", "SellerSuffice");
                options.Conventions.AuthorizeFolder("/Customer_Order(s)/", "SellerSuffice");
                options.Conventions.AuthorizeFolder("/Products/", "SellerSuffice");
                options.Conventions.AuthorizePage("/Payments/Index", "SellerSuffice");
                options.Conventions.AuthorizePage("/Payments/Details", "SellerSuffice");
                options.Conventions.AuthorizePage("/Orders/Edit", "SellerSuffice"); //Any users

                //Admin access
                options.Conventions.AuthorizeFolder("/Delivery_Services/", "AdminOnly");
                options.Conventions.AuthorizePage("/Payments/Create", "AdminOnly");
                options.Conventions.AuthorizePage("/Payments/Edit", "AdminOnly");
                options.Conventions.AuthorizePage("/Payments/Delete", "AdminOnly");
                options.Conventions.AuthorizePage("/Users/Create", "AdminOnly");
                options.Conventions.AuthorizePage("/Users/Delete", "AdminOnly");
                options.Conventions.AuthorizePage("/Users/Details", "AdminOnly");
                options.Conventions.AuthorizePage("/Users/Index", "AdminOnly");
                options.Conventions.AuthorizePage("/Orders/Create", "AdminOnly"); //Dunno rly co tu, no ale nie mog¹ sobie niezale¿nie tworzyæ zamówieñ bez poœrednicz¹cej strony
            });


            services.AddRazorPages();
            services.AddSession();
            services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestLocalization("en-US");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            //DO NOT TOUCH THE ORDER FOR THE LOVE OF EVERYTHING THATS SANE
            app.UseAuthentication();
            app.UseAuthorization();
            ///

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
