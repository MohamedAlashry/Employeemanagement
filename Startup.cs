using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;
        private ILogger<Startup>_log;

        public Startup(IConfiguration config, ILogger<Startup> log)
        {
            _config = config;
            _log = log;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<EmployeeDBContext>(options => options.UseSqlServer(_config.GetConnectionString("Conn")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength=8;
                }).AddEntityFrameworkStores<EmployeeDBContext>();
            services.AddMvc(options=> {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
                
            } );
            services.AddTransient<IGenericUnitOfWork, UnitOfWork>();
            services.Configure<IdentityOptions>(options => options.Password.RequireNonAlphanumeric = false);

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {

               app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/error/{0}");
                app.UseExceptionHandler("/error");

            }
            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            //FileServerOptions options = new FileServerOptions();
            //options.DefaultFilesOptions.DefaultFileNames.Clear();
            //options.DefaultFilesOptions.DefaultFileNames.Add("foo");
            //app.UseFileServer();
            //app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
            app.UseAuthentication();              
            app.UseMvc(routes => {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(env.EnvironmentName);

            //});
        }
    }
}
