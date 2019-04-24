namespace ngordat.net.backend.api.Settings
{
    /// <summary>
    /// AppSettings class.
    /// Used to retrieve AppSettings section from appSettings.json
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the JWT secret key used by the application.
        /// </summary>
        public string JWTSecret { get; set; }
    }
}
