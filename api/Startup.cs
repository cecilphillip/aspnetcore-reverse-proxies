using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace api
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/api", context =>
               {
                   context.Response.ContentType ??= "application/json";

                   return JsonSerializer.SerializeAsync(context.Response.Body, new { message = "hello world" });
               });

                endpoints.MapFallback(context => context.Response.WriteAsync("end of the road"));
            });
        }
    }
}
