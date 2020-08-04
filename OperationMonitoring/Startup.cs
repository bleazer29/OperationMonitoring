using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using OperationMonitoring.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OperationMonitoring.Hubs;
using Microsoft.AspNetCore.Http.Connections;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using OperationMonitoring.ModelsIdentity.Security;

namespace OperationMonitoring
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

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OperationMonitorDB")));

            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            }).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(o=>o.TokenLifespan = TimeSpan.FromHours(8));

            services.AddSingleton<DataProtectionPurposeStrings>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<DataFilterHub>("/data",
                    options =>
                    {
                        options.ApplicationMaxBufferSize = 64;
                        options.TransportMaxBufferSize = 64;
                        options.LongPolling.PollTimeout = TimeSpan.FromSeconds(60);
                        options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets;
                    }
                    );
            });
        }
    }
}