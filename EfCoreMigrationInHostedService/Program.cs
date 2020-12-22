using System;
using System.Threading;
using System.Threading.Tasks;
using EfCoreMigrationInHostedService.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EfCoreMigrationInHostedService
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                                   {
                                       services.AddHostedService<App>();
                                   });
    }

    public class App : IHostedService
    {
        private readonly ILogger<App> _logger;

        private readonly IHostApplicationLifetime _appLifetime;

        private readonly IConfiguration _configuration;

        private readonly IServiceCollection _serviceCollection;

        public App(ILogger<App>             logger,
                   IHostApplicationLifetime appLifetime,
                   IConfiguration           configuration)
        {
            _logger        = logger;
            _appLifetime   = appLifetime;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("App Start at: {time}", DateTimeOffset.Now);

            var contextFactory = new TestContextFactory
                                 {
                                     Configuration = _configuration
                                 };
            await using (var dbContext = contextFactory.CreateDbContext(Array.Empty<string>()))
            {
                var db = dbContext.Database;
                await db.MigrateAsync(cancellationToken: cancellationToken);
            }

            _appLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("App stopped at: {time}", DateTimeOffset.Now);
            return Task.CompletedTask;
        }
    }
}
