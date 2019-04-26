namespace ngordat.net.backend.api.Settings
{
  using ngordat.net.backend.transversal.Settings;

  /// <summary>
  /// AppSettings class.
  /// Used to retrieve AppSettings section from appSettings.json
  /// </summary>
  public class AppSettings : IAppSettings
  {
    /// <summary>
    /// Gets or sets the JWT secret key used by the application.
    /// </summary>
    public string JWTSecret { get; set; }

    /// <summary>
    /// Gets or sets the salt used for the passwords.
    /// </summary>
    public string PasswordSalt { get; set; }
  }
}
