using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SQLMultiFlowWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region DBContext
            //services.AddDbContext<SQLMultyFlowWebContext>(options=>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("EFCoreSQLMFW"));
            //});
            #endregion DBContext

            #region Authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(optipns =>
                {
                    optipns.Cookie.Name = "LoginCookie";
                    optipns.Cookie.MaxAge = TimeSpan.FromDays(3.5);

                    optipns.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Accounts/Access/Login");
                });
            #endregion Authentication

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{area}/{controller}/{action}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseEndpoints(paths =>
            {
                paths.MapControllerRoute(name: "addLogin", pattern: "Accounts/AddLogin/Create");
                paths.MapControllerRoute(name: "login", pattern: "Accounts/Access/Login");
            });
        }
    }
}