namespace ngordat.net.backend.api.Controllers.Bookmarks
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using ngordat.net.backend.domains.Bookmarks;
  using ngordat.net.backend.services.Bookmarks;
  using System;

  /// <summary>
  /// The <see cref="Bookmark"/> API.
  /// </summary>
  [Authorize]
  [ApiController]
  [Route("api/bookmarks")]
  public class BookmarksController : ControllerBase
  {
    /// <summary>
    /// The Bookmark service.
    /// </summary>
    private IBookmarkService _bookmarkService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookmarksController"/> class.
    /// </summary>
    /// <param name="bookmarkService">The <see cref="IBookmarkService"/> used.</param>
    public BookmarksController(IBookmarkService bookmarkService)
    {
      _bookmarkService = bookmarkService;
    }

    // GET: api/Bookmarks
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(_bookmarkService.Get());
    }

    // GET: api/Bookmarks/5
    [Authorize]
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
      return Ok(_bookmarkService.Get(id));
    }

    // POST: api/Bookmarks
    [Authorize]
    [HttpPost]
    public IActionResult Post([FromBody] Bookmark authorization)
    {
      try
      {
        return Created(string.Empty, _bookmarkService.Create(authorization));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // PUT: api/Bookmarks/5
    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Put(string id, [FromBody] Bookmark authorization)
    {
      try
      {
        return Ok(_bookmarkService.Update(id, authorization));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // PUT: api/Bookmarks
    [Authorize]
    [HttpPut()]
    public IActionResult Put([FromBody] Bookmark authorization)
    {
      try
      {
        return Ok(_bookmarkService.Update(authorization.Id, authorization));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // DELETE: api/Bookmarks/5
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
      try
      {
        return Ok(_bookmarkService.Delete(id));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // DELETE: api/Bookmarks
    [Authorize]
    [HttpDelete()]
    public IActionResult Delete([FromBody] Bookmark authorization)
    {
      try
      {
        return Ok(_bookmarkService.Delete(authorization));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
  }
}