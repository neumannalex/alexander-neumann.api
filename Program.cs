using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using alexander_neumann.api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace alexander_neumann.api
{
    public class Program
    {
        public static IConfiguration Configuration
        {
            get
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

                var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{env}.json", optional: true)
                                .AddEnvironmentVariables();

                if (string.Equals(env, "Development", StringComparison.InvariantCultureIgnoreCase))
                {
                    builder.AddUserSecrets<Startup>();
                }

                return builder.Build();
            }
        }

        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(Configuration)
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                            .Enrich.FromLogContext()
                            .WriteTo.Console()
                            .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                var host = CreateHostBuilder(args).Build();

                await MigrateDb(host);

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(Configuration)
                    .UseSerilog()
                    .UseStartup<Startup>()
                    .ConfigureKestrel((context, options) =>
                    {
                        //options.ConfigureHttpsDefaults(o =>
                        //    o.ClientCertificateMode = Microsoft.AspNetCore.Server.Kestrel.Https.ClientCertificateMode.RequireCertificate
                        //    );
                        //options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(20);
                        options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);
                        options.Limits.MaxRequestBodySize = null;
                    });
                });

        private static async Task MigrateDb(IHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            if (scopeFactory != null)
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    try
                    {
                        var context = scope.ServiceProvider.GetService<AppDbContext>();
                        await context.Database.MigrateAsync();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Error while migrating DB.", ex);
                    }
                }
            }
            else
            {
                Log.Information("Cannot create IServiceScopeFactory while migrating database.");
            }
        }
    }
}
