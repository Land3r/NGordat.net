namespace ngordat.net.backend.api.Controllers.Users
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;
  using ngordat.net.backend.domains.Users;
  using System;
  using IAuthorizationService = services.Users.IAuthorizationService;

  /// <summary>
  /// The <see cref="Authorization"/> API.
  /// </summary>
  [Authorize]
  [ApiController]
  [Route("api/authorizations")]
  public class AuthorizationsController : LoggedController<AuthorizationsController>
  {
    /// <summary>
    /// The Authorization service.
    /// </summary>
    private IAuthorizationService _authorizationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizationsController"/> class.
    /// </summary>
    /// <param name="logger">The logger used.</param>
    /// <param name="authorizationService">The <see cref="IAuthorizationService"/> used.</param>
    public AuthorizationsController(ILogger<AuthorizationsController> logger, IAuthorizationService authorizationService) : base(logger)
    {
      _authorizationService = authorizationService;
    }

    // GET: api/Authorizations
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(_authorizationService.Get());
    }

    // GET: api/Authorizations/5
    [Authorize]
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
      return Ok(_authorizationService.Get(id));
    }

    // POST: api/Authorizations
    [Authorize]
    [HttpPost]
    public IActionResult Post([FromBody] Authorization authorization)
    {
      try
      {
        return Created(string.Empty, _authorizationService.Create(authorization));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // PUT: api/Authorizations/5
    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Put(string id, [FromBody] Authorization authorization)
    {
      try
      {
        return Ok(_authorizationService.Update(id, authorization));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // PUT: api/Authorizations
    [Authorize]
    [HttpPut()]
    public IActionResult Put([FromBody] Authorization authorization)
    {
      try
      {
        return Ok(_authorizationService.Update(authorization.Id, authorization));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // DELETE: api/Authorizations/5
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
      try
      {
        return Ok(_authorizationService.Delete(id));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // DELETE: api/Authorizations
    [Authorize]
    [HttpDelete()]
    public IActionResult Delete([FromBody] Authorization authorization)
    {
      try
      {
        return Ok(_authorizationService.Delete(authorization));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
  }
}