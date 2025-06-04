namespace WakeUpServer;

using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Serilog;
using WakeUpServer.Logging;

internal class Startup
{
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddWakeUpServer();

        services.AddControllers();
        services.AddCommonServices();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("api", new OpenApiInfo { Title = "WakeUpServer API" });
            c.SwaggerDoc("web", new OpenApiInfo { Title = "WakeUpServer WEB" });
            c.ResolveConflictingActions(x => x.First());
            c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "WakeUpServer.Api.xml"));
        });

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseForwardedHeaders();
        }
        else
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseForwardedHeaders();
            app.UseHsts();
        }

        app.UseSerilogRequestLogging();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseSwagger(o =>
        {
            o.RouteTemplate = "swagger/{documentName}/swagger_v3.json";
            o.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
        });

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/api/swagger_v3.json", "WakeUpServer API");
            c.SwaggerEndpoint("/swagger/web/swagger_v3.json", "WakeUpServer WEB");
            c.RoutePrefix = "swagger";
            c.DisplayRequestDuration();
        });

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.UseMiddleware<RequestLoggingMiddleware>();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}