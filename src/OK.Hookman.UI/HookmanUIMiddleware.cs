using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OK.Hookman.UI.Config;

namespace OK.Hookman.UI
{
    public class HookmanUIMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HookmanUIConfig _config;
        private readonly (string Replace, string With)[] _uiPathReplacements = new (string replace, string with)[]
        {
            ("<base href=\"/\">", "<base href=\"{0}/\">"),
            ("styles.css", "{0}/styles.css"),
            ("runtime-es2015.js", "{0}/runtime-es2015.js"),
            ("polyfills-es2015.js", "{0}/polyfills-es2015.js"),
            ("runtime-es5.js", "{0}/runtime-es5.js"),
            ("polyfills-es5.js", "{0}/polyfills-es5.js"),
            ("scripts.js", "{0}/scripts.js"),
            ("main-es2015.js", "{0}/main-es2015.js"),
            ("main-es5.js", "{0}/main-es5.js"),
        };
        private static string IndexContent = string.Empty;

        public HookmanUIMiddleware(RequestDelegate next, HookmanUIConfig config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (string.IsNullOrEmpty(IndexContent))
            {
                var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var content = File.ReadAllText(Path.Combine(directory, "static/out/index.html"));

                foreach (var item in _uiPathReplacements)
                {
                    content = content.Replace(item.Replace, string.Format(item.With, "[[OK.Hookman.UI.Config.UIPath]]"));
                }

                content = content.Replace("[[OK.Hookman.UI.Config.UIPath]]", _config.UIPath);
                content = content.Replace("[[OK.Hookman.UI.Config.ApiUrl]]", _config.ApiUrl);

                IndexContent = content;
            }

            httpContext.Response.StatusCode = 200;
            httpContext.Response.ContentType = "text/html;charset=utf-8";

            await httpContext.Response.WriteAsync(IndexContent, Encoding.UTF8);
        }
    }
}