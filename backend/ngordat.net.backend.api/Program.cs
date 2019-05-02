namespace ngordat.net.backend.api
{
  using Microsoft.AspNetCore;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Logging;
  using ngordat.net.backend.api.Settings;
  using NLog.Web;
  using System;

  /// <summary>
  /// Program class.
  /// Application entry point.
  /// </summary>
  public class Program
  {
    /// <summary>
    /// Main entrypoint of the application
    /// </summary>
    /// <param name="args">The args passed to the application.</param>
    public static void Main(string[] args)
    {
      // Setup logger
      string nlogConfig = NLogSettings.NLogConfigFile;
      var logger = NLogBuilder.ConfigureNLog(nlogConfig).GetCurrentClassLogger();
      try
      {
        logger.Debug("Started program " + AppDomain.CurrentDomain.FriendlyName);
        CreateWebHostBuilder(args).Build().Run();
      }
      catch (Exception ex)
      {
        logger.Error(ex, "Stopped program because of exception");
        throw;
      }
      finally
      {
        NLog.LogManager.Shutdown();
      }
    }

    /// <summary>
    /// Creates a web host builder and configure it.
    /// </summary>
    /// <param name="args">The args passed to the application.</param>
    /// <returns>Thz <see cref="IWebHostBuilder"/> used to create the web hosts.</returns>
    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .ConfigureLogging(logging =>
        {
          logging.ClearProviders();
          logging.SetMinimumLevel(LogLevel.Trace);
        })
        .UseNLog();
    }
  }
}
