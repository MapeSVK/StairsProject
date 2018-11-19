using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using StairsAndShit.Core.Helpers;
using StairsAndShit.Infrastructure.Data;

namespace StairsAndShit.RestApi
{
    public class Startup
    {
	    public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
            JwtSecurityKey.SetSecret("a secret that needs to be at least 16 characters long");       
        }
        
        public IConfiguration Configuration { get; }        
        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JwtSecurityKey.Key,
                    ValidateLifetime = true, 
                    ClockSkew = TimeSpan.FromMinutes(5) 
                };
            });
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader()
                        .AllowAnyMethod());
            });
       
            if (Environment.IsDevelopment())
            {
                services.AddDbContext<StairsAppContext>(
                    opt => opt.UseSqlite("Data Source=stairsDB.db"));
            }
            else
            {
                services.AddDbContext<StairsAppContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            }
                    
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository<User>, UserRepository>();           
            
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService<User>, UserService>();
            
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
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<StairsAppContext>();
					DBSeed.SeedDB(ctx);
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
          
            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}