namespace ngordat.net.backend.transversal.Settings
{
  /// <summary>
  /// IAppSettings interface.
  /// Used to retrieve data from the configuration of the project.
  /// </summary>
  public interface IAppSettings
  {
    /// <summary>
    /// Gets or sets the JWT secret key used by the application.
    /// </summary>
    string JWTSecret { get; set; }

    /// <summary>
    /// Gets or sets the salt used for the passwords.
    /// </summary>
    string PasswordSalt { get; set; }
  }
}
