using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StairsAndShit.Core.ApplicationService;
using StairsAndShit.Core.ApplicationService.Impl;
using StairsAndShit.Core.DomainService;
using StairsAndShit.Core.Entity;
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
		        services.AddDbContext<StairsAppContext>(
			        opt => opt.UseSqlite("Data Source=stairsDB.db"));
	        }

	        else if (_env.IsProduction())
	        {
		        services.AddDbContext<StairsAppContext>(
			        opt => opt
				        .UseSqlServer(_conf.GetConnectionString("DefaultConnection")));
	        }
	        
	        /* SCOPES , DEPENDENCY INJECTION */
			   
	        services.AddScoped<IProductRepository, ProductRepository>();
	        services.AddScoped<IProductService, ProductService>();
	        services.AddScoped<IUserRepository<User>, UserRepository>();
	        
	        /* configure strongly typed settings objects */
	        var appSettingsSection = _conf.GetSection("AppSettings");
	        services.Configure<AppSettings>(appSettingsSection);
	        
			/* MVC SERIALIZER */
	        services.AddMvc().AddJsonOptions(options => {
		        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
	        });

	        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
	        
	        /* CORS - origin from front-end */
	        services.AddCors(options =>
	        {
		        options.AddPolicy("AllowSpecificOrigin",
			        builder => builder.AllowAnyOrigin().AllowAnyHeader()
				        .AllowAnyMethod());
	        });
	        
	        /* Auto mapper class added */
	        services.AddAutoMapper();
	        
	        
	        // configure JWT authentication
	        var appSettings = appSettingsSection.Get<AppSettings>();
	        var key = Encoding.ASCII.GetBytes(appSettings.Secret);
	        services.AddAuthentication(x =>
		        {
			        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		        })
		        .AddJwtBearer(x =>
		        {
			        x.Events = new JwtBearerEvents
			        {
				        OnTokenValidated = context =>
				        {
					        var userRepo = context.HttpContext.RequestServices.GetRequiredService<IUserRepository<User>>();
					        var userId = int.Parse(context.Principal.Identity.Name);
					        var user = userRepo.GetById(userId);
					        if (user == null)
					        {
						        // return unauthorized if user no longer exists
						        context.Fail("Unauthorized");
					        }
					        return Task.CompletedTask;
				        }
			        };
			        x.RequireHttpsMetadata = false;
			        x.SaveToken = true;
			        x.TokenValidationParameters = new TokenValidationParameters
			        {
				        ValidateIssuerSigningKey = true,
				        IssuerSigningKey = new SymmetricSecurityKey(key),
				        ValidateIssuer = false,
				        ValidateAudience = false
			        };
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

            //app.UseHttpsRedirection();
            app.UseHttpsRedirection();

	        app.UseCors("AllowSpecificOrigin");
            app.UseMvc();
        }
    }
}