using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JacksonVeroneze.ViaCep.BuildingBlocks;
using JacksonVeroneze.ViaCep.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace JacksonVeroneze.ViaCep.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = Logger.FactoryLogger();

            Log.Information("Starting up");

            IHost host = CreateHostBuilder(args).Build();

            Log.Information("Performing migrations.");

            using IServiceScope scope = host.Services.CreateScope();

            DatabaseContext db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            db.Database.Migrate();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog();
                });
    }
}
