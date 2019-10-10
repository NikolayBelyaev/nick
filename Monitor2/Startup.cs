using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monitor2.DAL;
using Monitor2.Models;
using Monitor2.Services;
using Monitor2.Services.Implementation;

namespace Monitor2
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var cs = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];

            services.AddDbContext<MonitorDBContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(cs));

            services.Configure<RequestHeaderOptions>(Configuration.GetSection("RequestHeader"));
            services.Configure<ServicesOptions>(Configuration.GetSection("ServicesOptions"));
            services.Configure<BackgroundServiceOptions>(Configuration.GetSection("BackgroundServiceOptions"));

            services.AddHttpClient();

            services.AddTransient<HttpService>();
            services.AddTransient<IDBService, DBService>();
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<IRequestDBService, RequestDBService>();
            services.AddTransient<IRequestService, RequestService>();
            services.AddHostedService<BackService>();
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            SetupDatabase(app);
        }

        private void SetupDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MonitorDBContext>();
                context.Database.Migrate();

                if (!context.Services.Any())
                {
                    context.Services.Add(new DAL.Entities.ServiceEntity { Url = "http://ibonus.1c-work.net/api/ibonus/version" });
                    context.SaveChanges();
                }
            }
        }
    }
}
