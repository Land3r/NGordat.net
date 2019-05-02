namespace ngordat.net.backend.api.Controllers
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;
  using ngordat.net.backend.transversal.Logs;
  using System.Linq;
  using System.Reflection;
  using System.Text;

  /// <summary>
  /// LoggedController class.
  /// Used to provide logging mechanics to <see cref="Controller"/>.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class LoggedController<T> : ControllerBase
  {
    /// <summary>
    /// The instance of the logger used.
    /// </summary>
    private readonly ILogger<T> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggedController{T}"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> used to log.</param>
    public LoggedController(ILogger<T> logger)
    {
      _logger = logger;
    }

    /// <summary>
    /// Logs the message.
    /// </summary>
    /// <param name="level">The log criticity.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The args to log.</param>
    protected void Log(LogLevel level, string message, params string[] args)
    {
      _logger.Log(level, message, args);
    }

    /// <summary>
    /// Logs an object
    /// </summary>
    /// <param name="obj">The object to log.</param>
    /// <param name="level">(Optional) The log criticity.</param>
    protected void LogObject(object obj, LogLevel level = LogLevel.Trace)
    {
      _logger.Log(level, LogHelper.GetObjectToString(obj));
    }
  }
}
