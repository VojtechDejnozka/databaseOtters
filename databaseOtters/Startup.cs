using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using databaseOtters.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;

namespace databaseOtters
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
            services.AddDbContext<OtterDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(o =>
                {
                    o.SignIn.RequireConfirmedEmail = false;
                    o.SignIn.RequireConfirmedAccount = false;
                    o.Password.RequiredLength = 2;
                    o.Password.RequireDigit = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                }).AddEntityFrameworkStores<OtterDbContext>();
            services.AddRazorPages(o => {
                o.Conventions.AuthorizePage("/Create");
                o.Conventions.AuthorizePage("/Edit");
                o.Conventions.AuthorizePage("/Delete");

                o.Conventions.AuthorizePage("/Locations/Create");
                o.Conventions.AuthorizePage("/Locations/Edit");
                o.Conventions.AuthorizePage("/Locations/Delete");

                o.Conventions.AuthorizePage("/Places/Create");
                o.Conventions.AuthorizePage("/Places/Edit");
                o.Conventions.AuthorizePage("/Places/Delete");
            }
            );
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
                endpoints.MapRazorPages();
            });
        }
    }
}
