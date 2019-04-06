using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Filters;
using WebStore.Infrastructure.Implementations;
using WebStore.Infrastructure.Interfaces;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration Configuration) => this.Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddSingleton<IProductData, InMemoryProductData>();
            //services.AddScoped<>()
            //services.AddTransient<>();

            services.AddMvc(opt =>
            {
                //opt.Filters.Add<ActionFilter>();
                //opt.Conventions.Add(new TestConvention());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //app.UseWelcomePage("/Welcome");

            //app.UseMvcWithDefaultRoute(); // "default" : "{controller=Home}/{action=Index}/{id?}"
            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"/*,
                    defaults: new
                    {
                        controller = "Home", 
                        action = "Index",
                        id = (int?)null
                    }*/);
            });
        }
    }
}
