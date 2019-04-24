using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ngordat.net.backend.api
{
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
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
