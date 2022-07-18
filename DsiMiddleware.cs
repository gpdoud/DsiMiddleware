using System;
namespace DsiMiddleware {
    public class DsiMiddlewareApiToken {

        private readonly RequestDelegate _next;

        public DsiMiddlewareApiToken(RequestDelegate next) {
            _next = next;
        }

        private IConfigurationRoot GetAppsettings()
            => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


        public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext http) {
            var token = GetAppsettings().GetValue<string>("AppSettings:token");
            var apikey = http.Request.Headers["apikey"];
            if (token == apikey) {
                await _next(http);
            } else {
                http.Response.StatusCode = 400;
            }
        }
    }

    public static class DsiMiddlewareExtensionMethods {

        public static void UseDsiMiddleware(this WebApplication app) {
            app.UseMiddleware<DsiMiddleware.DsiMiddlewareApiToken>();
        }
    }
}

