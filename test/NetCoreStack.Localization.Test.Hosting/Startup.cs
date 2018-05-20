using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using NetCoreStack.Mvc;

namespace NetCoreStack.Localization.Test.Hosting
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
            services.AddNetCoreStackMvc(options =>
            {
                options.AppName = "NetCoreStack Localization";
            });

            services.AddMvc();
            services.AddNetCoreStackLocalization(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseNetCoreStackMvc();
            app.UseNetCoreStackLocalization();

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //   name: "culture",
                //   template: "{culture:regex(^[a-z]{{2}}-[A-Z]{{2}}$)=" + CultureInfo.CurrentCulture.Name + "}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            
        }
    }
}
