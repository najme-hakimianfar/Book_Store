using Book_Store.CoreLayer.Services;
using Book_Store.CoreLayer.Services.Books;
using Book_Store.CoreLayer.Services.Comments;
using Book_Store.CoreLayer.Services.Ctegories;
using Book_Store.CoreLayer.Services.FileManager;
using Book_Store.CoreLayer.Services.Orders;
using Book_Store.CoreLayer.Services.Users;
using Book_Store.DataLayer.Context;
using Book_Store.DataLayer.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.web
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
            services.AddControllersWithViews();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IMainPageService, MainPageService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddDbContext<DataBaseContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("Defualt"));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(UserRole.Admin.ToString(), policy => policy.RequireRole(UserRole.Admin.ToString()));
                options.AddPolicy(UserRole.Customer.ToString(), policy => policy.RequireRole(UserRole.Customer.ToString()));
            });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(option =>
            {
                option.LoginPath = "/Auth/Login";
                option.LogoutPath = "/Auth/Logout";
                option.ExpireTimeSpan = TimeSpan.FromDays(30);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapRazorPages();
            });
        }
    }
}
