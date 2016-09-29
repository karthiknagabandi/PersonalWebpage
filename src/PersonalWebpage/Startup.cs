using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonalWebpage.Service;
using Microsoft.Extensions.Configuration;
using PersonalWebpage.Models;

namespace PersonalWebpage
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            _config = builder.Build();

        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Dependency Injection
            //Registering own services
            // AddTransient - Transient creates an instance of debugmail service when needed, and keeps is cached around              
            //services.AddTransient<IMailService, DebugMailService>();

            //AddScoped - create an instance of debugmail for each set of requests and reused during the requests
            //AddSingleton  - create on instance the first time we needed and pass that instance over and over again
            services.AddSingleton(_config);

            if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
            {
                services.AddScoped<IMailService, DebugMailService>();
            }
            else
            {
                // Implement the service
            }

            //wiring up EF interface and also wiring Context
            // context now is injectable to different parts of the project
            services.AddDbContext<WorldContext>();

            services.AddTransient<WorldContextSeedData>();


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ILoggerFactory loggerFactory, WorldContextSeedData seeder)
        {
            //MiddleWare

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(config => {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index"}
                    );
            });

            // calling wait to make is a sync process
            seeder.EnsureSeedData().Wait(); 
        }
    }
}
