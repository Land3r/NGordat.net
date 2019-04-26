using System;
using System.Collections.Generic;
using System.Text;

namespace ngordat.net.backend.transversal.Settings
{
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
