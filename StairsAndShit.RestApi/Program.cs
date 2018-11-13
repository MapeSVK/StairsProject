using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StairsAndShit.Infrastructure.Data;

namespace StairsAndShit.RestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
	        // Build host
	        var host = CreateWebHostBuilder(args).Build();

	        // Initialize the database
	        using (var scope = host.Services.CreateScope())
	        {
		        var services = scope.ServiceProvider;
		        var dbContext = services.GetService<StairsAppContext>();
		        DBInitializer.DbInitializer.Initialize(dbContext);
	        }

	        // Run host
	        host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}