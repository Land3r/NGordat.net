namespace ngordat.net.backend.api.Controllers.Users
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using ngordat.net.backend.domains;
  using ngordat.net.backend.services.Users;
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// The <see cref="UserGroup"/> API.
  /// </summary>
  [Authorize]
  [ApiController]
  [Route("api/usergroups")]
  public class UserGroupsController : ControllerBase
  {
    /// <summary>
    /// The UserGroup service.
    /// </summary>
    private IUserGroupService _userGroupService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserGroupsController"/> class.
    /// </summary>
    /// <param name="userGroupService">The <see cref="IUserGroupService"/> used.</param>
    public UserGroupsController(IUserGroupService userGroupService)
    {
      _userGroupService = userGroupService;
    }

    // GET: api/Usergroups
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(_userGroupService.Get());
    }

    // GET: api/Usergroups/5
    [Authorize]
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
      return Ok(_userGroupService.Get(id));
    }

    // POST: api/Usergroups
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Post([FromBody] UserGroup userGroup)
    {
      try
      {
        return Created(string.Empty, _userGroupService.Create(userGroup));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // PUT: api/Usergroups/5
    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Put(string id, [FromBody] UserGroup userGroup)
    {
      try
      {
        return Ok(_userGroupService.Update(id, userGroup));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // PUT: api/Usergroups
    [Authorize]
    [HttpPut()]
    public IActionResult Put([FromBody] UserGroup userGroup)
    {
      try
      {
        return Ok(_userGroupService.Update(userGroup.Id, userGroup));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // DELETE: api/Usergroups/5
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
      try
      {
        return Ok(_userGroupService.Delete(id));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // DELETE: api/Usergroups
    [Authorize]
    [HttpDelete()]
    public IActionResult Delete([FromBody] UserGroup userGroup)
    {
      try
      {
        return Ok(_userGroupService.Delete(userGroup));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
  }
}
