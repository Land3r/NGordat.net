namespace ngordat.net.backend.api.Controllers.Security
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;
  using ngordat.net.backend.security;
  using System;
  using System.Collections.Generic;
  using System.Security.Cryptography;
  using System.Text;

  /// <summary>
  /// Security controller api.
  /// </summary>
  [Authorize]
  [ApiController]
  [Route("api/security")]
  public class SecurityController : LoggedController<SecurityController>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityController"/> class.
    /// </summary>
    /// <param name="logger">The logger to use.</param>
    public SecurityController(ILogger<SecurityController> logger) : base(logger)
    {
    }

    /// <summary>
    /// Requests a new communication channel to be opened
    /// </summary>
    /// <param name="clientPublicKey">The client public key.</param>
    /// <returns>The server's public key generated for this channel, encrypted with the client public key.</returns>
    [Authorize]
    [HttpPost]
    public IActionResult RequestChannel(string clientPublicKey)
    {
      // Generate a new Private/Public keypair for the user session.
      (RSAParameters serverPrivateKey, RSAParameters serverPublicKey) = SecurityProvider.GeneratePrivatePublicPair();
      // Converts to string
      string serverPrivateStringKey = SecurityProvider.KeyToString(serverPrivateKey);
      string serverPublicStringKey = SecurityProvider.KeyToString(serverPublicKey);

      // Store server private key, and client public key in user session
      HttpContext.Session.Set(SessionSecurityConsts.CLIENT_PUBLIC_KEY, Encoding.Unicode.GetBytes(clientPublicKey));
      HttpContext.Session.Set(SessionSecurityConsts.SERVER_PRIVATE_KEY, Encoding.Unicode.GetBytes(serverPrivateStringKey));
      HttpContext.Session.Set(SessionSecurityConsts.SERVER_PUBLIC_KEY, Encoding.Unicode.GetBytes(serverPublicStringKey));

      // Return server's public key, encrypted with client's public key.
      string cipheredPubKey = SecurityProvider.Encrypt(clientPublicKey, serverPublicStringKey);
      return Ok(cipheredPubKey);
    }
  }
}
