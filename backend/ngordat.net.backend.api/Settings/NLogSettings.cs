namespace ngordat.net.backend.api.Settings
{
  public class NLogSettings
  {
    public static string NLogConfigFile {
      get
      {
#if DEBUG
        return "nlog.Development.config";
#else
      return "nlog.Release.config";
#endif
      }
    }
  }
}
