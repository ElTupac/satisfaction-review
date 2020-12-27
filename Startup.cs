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
using satisfaction_review.Models;
using Microsoft.EntityFrameworkCore;

namespace satisfaction_review
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
            services.AddControllersWithViews();

            services.AddDbContext<ReviewContext>(options =>
            {
                var uriConnection = (Environment.GetEnvironmentVariable("DATABASE_URL")).Split("://")[1];
                string userAndPass = uriConnection.Split("@")[0];
                string dbInfo = uriConnection.Split("@")[1];
                string connectionString = "SSL Mode=Require; Trust Server Certificate=true;";
                connectionString = "User ID=" + userAndPass.Split(":")[0] + ";Password=" + userAndPass.Split(":")[1]
                + ";Server=" + (dbInfo.Split("/")[0]).Split(":")[0] + ";Database=" + dbInfo.Split("/")[1] + ";" + connectionString;
                //Only HardCode the credentials when need to make a migration an you have to connect from local pc
                /* string connectionString = "User ID=ufxlfoobgktzzs;Password=18920978731068a3dde6154a636791cb7ced735d09994ef856ee119b6ec281d6;" +
                "Server=ec2-54-158-122-162.compute-1.amazonaws.com;Database=dfslbbpdvp4usq;" +
                "SSL Mode=Require; Trust Server Certificate=true;"; */
                options.UseNpgsql(connectionString);
            });
            
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.Name = ".reviews.Session";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
