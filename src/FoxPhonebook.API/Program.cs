using FoxPhonebook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FoxPhonebook.API;

public class Program
{
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var configuration = services.GetRequiredService<IConfiguration>();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel
            .Information()
            .Enrich.WithProperty("Service", "FoxPhonebook")
            .WriteTo.Console()
            .WriteTo.Debug()
            .CreateLogger();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();
            }

            await ApplicationDbContextSeed.StartupSeed(context);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            throw;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "An error occurred while startup");
            throw;
        }

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder
                    .UseStartup<Startup>())
                    .UseSerilog();
}
