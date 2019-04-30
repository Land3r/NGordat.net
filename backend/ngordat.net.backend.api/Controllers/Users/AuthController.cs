namespace ngordat.net.backend.api.Controllers.Users
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using ngordat.net.backend.api.Services.Users;
  using ngordat.net.backend.domains.Users;
  using ngordat.net.backend.services.Users;

  /// <summary>
  /// Authentication controller api.
  /// </summary>
  [Authorize]
  [ApiController]
  [Route("api/auth")]
  public class AuthController : ControllerBase
  {
    /// <summary>
    /// The Auth service.
    /// </summary>
    private IAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="userService">The <see cref="IUserService">User service.</see></param>
    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost()]
    public IActionResult Authenticate([FromBody]User userParam)
    {
      User user = _authService.Authenticate(userParam.Username, userParam.Password);

      if (user == null)
        return BadRequest(new { error = "Username or password is incorrect." });

      return Ok(user);
    }
  }
}