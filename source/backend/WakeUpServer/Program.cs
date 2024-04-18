namespace WakeUpServer;

using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using WakeUpServer.BackgroundServices;

public static class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                "./../logs/WakeUpServer-.log",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {CorrelationId} {Level:u3}] {Username} {Message:lj}{NewLine}{Exception}",
                fileSizeLimitBytes: 10 * 1024 * 1024,
                retainedFileCountLimit: 10,
                rollOnFileSizeLimit: true,
                shared: true,
                flushToDiskInterval: TimeSpan.FromSeconds(1))
            .CreateLogger();

        string? currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Log.Information("Setting the current directory to '{CurrentDirectory}'", currentDirectory);
        Directory.SetCurrentDirectory(currentDirectory!);
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        string contentDirectory = $"{DirectoryProvider.ResolveContentDirectory()}/wwwroot";
        Log.Information("Setting the web root directory to '{ContentDirectory}'", contentDirectory);

        return Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureServices((_, services) => services.AddHostedService<BackgroundServiceHost>())
            .ConfigureWebHostDefaults(webBuilder => webBuilder
                .UseKestrel()
                .UseUrls("http://*:5000")
                .UseStartup<Startup>()
                .UseWebRoot(contentDirectory));
    }
}