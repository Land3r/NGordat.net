namespace ngordat.net.backend.api.Controllers.Users
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;
  using ngordat.net.backend.domains.Users;
  using ngordat.net.backend.services.Users;
  using System;

  /// <summary>
  /// The <see cref="User"/> API.
  /// </summary>
  [Authorize]
  [ApiController]
  [Route("api/users")]
  public class UsersController : LoggedController<UsersController>
  {
    /// <summary>
    /// The User service.
    /// </summary>
    private IUserService _userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersController"/> class.
    /// </summary>
    /// <param name="logger">The logger used.</param>
    /// <param name="userService">The <see cref="IUserService"/> used.</param>
    public UsersController(ILogger<UsersController> logger, IUserService userService) : base(logger)
    {
      _userService = userService;
    }

    // GET: api/Users
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(_userService.Get());
    }

    // GET: api/Users/5
    [Authorize]
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
      return Ok(_userService.Get(id));
    }

    // POST: api/Users
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
      try
      {
        return Created(string.Empty, _userService.Create(user));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // PUT: api/Users/5
    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Put(string id, [FromBody] User user)
    {
      try
      {
        return Ok(_userService.Update(id, user));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // PUT: api/Users
    [Authorize]
    [HttpPut()]
    public IActionResult Put([FromBody] User user)
    {
      try
      {
        return Ok(_userService.Update(user.Id, user));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // DELETE: api/Users/5
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
      try
      {
        return Ok(_userService.Delete(id));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // DELETE: api/Users
    [Authorize]
    [HttpDelete()]
    public IActionResult Delete([FromBody] User user)
    {
      try
      {
        return Ok(_userService.Delete(user));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
  }
}
