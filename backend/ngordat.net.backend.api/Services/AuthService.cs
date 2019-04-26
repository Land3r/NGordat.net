using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ngordat.net.backend.api.Settings;
using ngordat.net.backend.domains.Users;
using ngordat.net.backend.services.Users;
using ngordat.net.backend.transversal.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ngordat.net.backend.api.Services
{
  /// <summary>
  /// Auth Service class.
  /// </summary>
  public class AuthService : IAuthService
  {
    /// <summary>
    /// The <see cref="IUserService"/> used to retrieve users.
    /// </summary>
    private IUserService _userService;

    /// <summary>
    /// The JWTSecret used on this instance.
    /// </summary>
    private string _JWTSecret;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class.
    /// </summary>
    /// <param name="appSettings">The <see cref="AppSettings"/> used.</param>
    /// <param name="userService">The <see cref="IUserService"/> used.</param>
    public AuthService(IOptions<AppSettings> appSettings,
        IUserService userService)
    {
      _JWTSecret = appSettings.Value.JWTSecret;
      _userService = userService;
    }

    /// <summary>
    /// Tries to authenticates a <see cref="User"/> based on it's
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public User Authenticate(string username, string password)
    {
      var user = _userService.Get(username, password);

      // return null if user not found
      if (user == null)
        return null;

      // authentication successful so generate jwt token
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_JWTSecret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
            new Claim(ClaimTypes.Name, user.Id.ToString())
          }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      user.Token = tokenHandler.WriteToken(token);

      // Remove password before returning
      user = UserService.GetSanitized(user);

      return user;
    }

  }
}
