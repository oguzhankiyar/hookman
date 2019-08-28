using System;
using Microsoft.Extensions.DependencyInjection;
using OK.Hookman.Client.Config;
using OK.Hookman.Client.Factory;

namespace OK.Hookman.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHookmanClient(this IServiceCollection services, Action<HookmanClientConfig> configAction)
        {
            var config = new HookmanClientConfig();
            configAction.Invoke(config);

            return AddHookmanClient(services, config);
        }

        public static IServiceCollection AddHookmanClient(this IServiceCollection services, HookmanClientConfig config)
        {
            services.AddSingleton<HookmanClientConfig>(_ => config);

            services.AddTransient<IHookmanClientFactory, HookmanClientFactory>();

            return services;
        }
    }
}