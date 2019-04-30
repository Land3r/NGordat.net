namespace ngordat.net.backend.api
{
  using Microsoft.AspNetCore;
  using Microsoft.AspNetCore.Hosting;

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
      CreateWebHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Creates a web host builder and configure it.
    /// </summary>
    /// <param name="args">The args passed to the application.</param>
    /// <returns>Thz <see cref="IWebHostBuilder"/> used to create the web hosts.</returns>
    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();
    }
  }
}
