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
using StairsAndShit.Core.Entity;
using StairsAndShit.Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;


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
		    
		    // jwt security for authentication
		    JwtSecurityKey.SetSecret("My secret key");
	    }
	    
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	        
	        // JWT AUTHENTICATION
	        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
	        {
		        options.TokenValidationParameters = new TokenValidationParameters
		        {
			        ValidateAudience = false,
			        ValidateIssuer = false,
			        ValidateIssuerSigningKey = true,
			        IssuerSigningKey = JwtSecurityKey.Key,
			        ValidateLifetime = true, //validate the expiration and not before values in the token
			        ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
		        };
	        });
	        
	        if (_env.IsDevelopment())
	        {		        
		        services.AddDbContext<StairsAppContext>(
			        opt => opt.UseSqlite("Data Source=customerApp.db"));
	        }

	        else if (_env.IsProduction())
	        {
		        services.AddDbContext<StairsAppContext>(
			        opt => opt
				        .UseSqlServer(_conf.GetConnectionString("DefaultConnection")));
	        }
			   
	        services.AddScoped<IProductRepository, ProductRepository>();
	        services.AddScoped<IProductService, ProductService>();
	        services.AddScoped<IUserRepository<User>, UserRepository>();
	        
	        

	        services.AddMvc().AddJsonOptions(options => {
		        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
	        });
			
	        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
	        
	        services.AddCors(options =>
	        {
		        options.AddPolicy("AllowSpecificOrigin",
			        builder => builder.AllowAnyOrigin().AllowAnyHeader()
				        .AllowAnyMethod());
	        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
	            app.UseDeveloperExceptionPage();
	            using (var scope = app.ApplicationServices.CreateScope())
	            {
		            var ctx = scope.ServiceProvider.GetService<StairsAppContext>();
		            ctx.Database.EnsureDeleted();
		            ctx.Database.EnsureCreated();
	            }
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

	        /* USAGE - Calling */
            app.UseHttpsRedirection();
	        app.UseCors("AllowSpecificOrigin");
            app.UseMvc();
	        
	        // Use authentication
	        app.UseAuthentication();
        }
    }
}