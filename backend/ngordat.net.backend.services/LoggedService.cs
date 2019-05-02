namespace ngordat.net.backend.services
{
  using Microsoft.Extensions.Logging;
  using ngordat.net.backend.transversal.Logs;

  public abstract class LoggedService<T>
  {
    /// <summary>
    /// The instance of the logger used.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Logger{T}"/> to instanciate.</typeparam>
    private readonly ILogger<T> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggedService{T}"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> used to log.</param>
    public LoggedService(ILogger<T> logger)
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
