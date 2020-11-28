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
                options.Conventions.AuthorizeFolder("/Admin/", "AdminOnly");
                options.Conventions.AuthorizeFolder("/Seller/", "SellerSuffice");
                options.Conventions.AuthorizeFolder("/User/", "UserSuffice");
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
