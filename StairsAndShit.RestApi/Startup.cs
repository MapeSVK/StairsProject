using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StairsAndShit.Core.ApplicationService;
using StairsAndShit.Core.ApplicationService.Impl;
using StairsAndShit.Core.DomainService;
using StairsAndShit.Infrastructure.Data;

namespace StairsAndShit.RestApi
{
    public class Startup
    {
	    private IConfiguration _conf { get; }

	    private IHostingEnvironment _env { get; set; }

	    public Startup(IHostingEnvironment env)
	    {
		    _env = env;
		    var builder = new ConfigurationBuilder()
			    .SetBasePath(env.ContentRootPath)
			    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
			    .AddEnvironmentVariables();
		    _conf = builder.Build();
	    }
	    

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	        if (_env.IsDevelopment())
	        {
		        Console.WriteLine("Data could not be obtained from Database. App is running on Development mode." +
		                          "Look at the Startup file");
	        }

	        else if (_env.IsProduction())
	        {
		        services.AddDbContext<StairsAppContext>(
			        opt => opt.UseSqlServer(_conf.GetConnectionString("DefaultConnection")));
	        }
			
	      
	        services.AddScoped<IProductRepository, ProductRepository>();
	        //services.AddScoped<IProductService, ProductService>();
	        

	        services.AddMvc().AddJsonOptions(options => {
		        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
	        });
			
	        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
				Console.WriteLine("Data could not be obtained from Database. App is running on Development mode." +
				                  "Look at the Startup file");
                app.UseDeveloperExceptionPage();
            }
            else
            {
	            using (var scope = app.ApplicationServices.CreateScope())
	            {
		            var ctx = scope.ServiceProvider.GetService<StairsAppContext>();
		            ctx.Database.EnsureCreated();
	            }
	            app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}