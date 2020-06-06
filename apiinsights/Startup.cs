using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Steeltoe.Discovery.Client;
using Steeltoe.Management.Endpoint.Env;
using Steeltoe.Management.Endpoint.Metrics;
using Steeltoe.Management.Endpoint.Refresh;


namespace apiinsights
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddPrometheusActuator(Configuration);
            services.AddEnvActuator(Configuration);
            services.AddRefreshActuator(Configuration);
            services.AddDiscoveryClient(Configuration);

        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseRefreshActuator();
            app.UsePrometheusActuator();
            app.UseEnvActuator();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    }
                });

                endpoints.MapGet("/api", context =>
                {
                    context.Response.ContentType ??= "application/json";

                    return JsonSerializer.SerializeAsync(context.Response.Body, new { message = "hello world" });
                });

                endpoints.MapFallback(context => context.Response.WriteAsync("end of the road"));
            });

            app.UseDiscoveryClient();
        }
    }
}
