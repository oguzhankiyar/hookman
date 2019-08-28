using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OK.Hookman.Persistence.Core.Repositories;
using OK.Hookman.Persistence.SqlServer.Config;
using OK.Hookman.Persistence.SqlServer.Constants;
using OK.Hookman.Persistence.SqlServer.Contexts;
using OK.Hookman.Persistence.SqlServer.Repositories;

namespace OK.Hookman.Persistence.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHookmanSqlServerPersistence(this IServiceCollection services, Action<HookmanSqlServerPersistenceConfig> configAction)
        {
            var config = new HookmanSqlServerPersistenceConfig();
            configAction.Invoke(config);

            return AddHookmanSqlServerPersistence(services, config);
        }

       public static IServiceCollection AddHookmanSqlServerPersistence(this IServiceCollection services, HookmanSqlServerPersistenceConfig config)
       {
            services.AddDbContextPool<HookmanDataContext>(options =>
            {
                options.UseSqlServer(config.ConnectionString, sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.CommandTimeout(120).EnableRetryOnFailure();
                    sqlServerOptionsAction.MigrationsHistoryTable(TableConstants.MigrationHistoryTableName, TableConstants.SchemaName);
                });
            });
            
            services.AddTransient<IActionRepository, ActionRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IHookRepository, HookRepository>();
            services.AddTransient<IReceiverRepository, ReceiverRepository>();
            services.AddTransient<ISenderRepository, SenderRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();

            return services;
        }

        public static IApplicationBuilder UseHookmanSqlServerPersistence(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<HookmanDataContext>()?.Database.Migrate();
            }

            return app;
        }
    }
}