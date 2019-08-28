using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using OK.Hookman.UI.Config;

namespace OK.Hookman.UI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHookmanUI(this IServiceCollection services, Action<HookmanUIConfig> configAction)
        {
            var config = new HookmanUIConfig();
            configAction.Invoke(config);

            return AddHookmanUI(services, config);
        }

        public static IServiceCollection AddHookmanUI(this IServiceCollection services, HookmanUIConfig config)
        {
            config.UIPath = "/" + config.UIPath.Trim('/');
            config.ApiUrl = config.ApiUrl.TrimEnd('/');

            services.AddSingleton<HookmanUIConfig>(_ => config);

            return services;
        }

        public static IApplicationBuilder UseHookmanUI(this IApplicationBuilder app)
        {
            var config = app.ApplicationServices.GetService<HookmanUIConfig>();
            var directory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "static/out");

            app.Map(config.UIPath, _app =>
            {
                _app.UseDefaultFiles(new DefaultFilesOptions()
                {
                    DefaultFileNames = new List<string>()
                    {
                        "index.html"
                    }
                });

                _app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(directory),
                    RequestPath = string.Empty
                });

                _app.UseMiddleware<HookmanUIMiddleware>();
            });

            return app;
        }
    }
}