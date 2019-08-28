using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OK.Hookman.API.Config;
using OK.Hookman.API.Filters;

namespace OK.Hookman.API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHookmanAPI(this IServiceCollection services, Action<HookmanAPIConfig> configAction)
        {
            var config = new HookmanAPIConfig();
            configAction.Invoke(config);

            return AddHookmanAPI(services, config);
        }

        public static IServiceCollection AddHookmanAPI(this IServiceCollection services, HookmanAPIConfig config)
        {
            config.ApiPath = "/" + config.ApiPath.Trim('/');

            services.AddCors((options) =>
            {
                options.AddPolicy("DefaultPolicy", (policy) =>
                {
                    policy.AllowAnyOrigin()
                          .AllowCredentials()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            services.AddSingleton<HookmanAPIConfig>(_ => config);

            services.AddMvc(opt =>
                    {
                        opt.Filters.Add(new GlobalExceptionFilter());
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;
        }

        public static IApplicationBuilder UseHookmanAPI(this IApplicationBuilder app)
        {

            var config = app.ApplicationServices.GetService<HookmanAPIConfig>();

            app.Map(config.ApiPath, (_app) =>
            {
                _app.UseCors("DefaultPolicy");
                _app.UseMvcWithDefaultRoute();
            });

            return app;
        }
    }
}