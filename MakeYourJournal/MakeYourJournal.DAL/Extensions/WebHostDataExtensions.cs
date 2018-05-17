using MakeYourJournal.DAL.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace MakeYourJournal.DAL.Extensions
{
    public static class WebHostDataExtensions
    {
        #region Extension methods

        public static IWebHost MigrateDatabase<TContext>(this IWebHost host)
            where TContext : DbContext
                => Scoped<JournalDbContext>(host, (s, l) => s.GetRequiredService<TContext>().Database.Migrate(), "Migrating database");

        public static IWebHost Seed<TSeeder, TContext, TData>(this IWebHost host)
            where TSeeder : IDataSeeder<TContext, TData>
            where TContext : DbContext
                => Scoped<TSeeder>(
                    host,
                    (s, l) => l.LogInformation(
                        $"Seeded {s.GetRequiredService<TSeeder>().SeedData(s.GetRequiredService<TContext>(), () => s.GetRequiredService<TData>())} entities."
                    ),
                    "Seed database");

        #endregion

        #region Scoping

        private static IWebHost Scoped<TLog>(this IWebHost host, Action<IServiceProvider, ILogger<TLog>> action, string title)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var logger = serviceProvider.GetRequiredService<ILogger<TLog>>();
                try
                {
                    action(serviceProvider, logger);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred during action: {title}");
                }
            }
            return host;
        }

        #endregion
    }
}