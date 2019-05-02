namespace ngordat.net.backend.api.Controllers.Bookmarks
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;
  using ngordat.net.backend.domains.Bookmarks;
  using ngordat.net.backend.services.Bookmarks;
  using System;

  /// <summary>
  /// The <see cref="BookmarkTag"/> API.
  /// </summary>
  [Authorize]
  [ApiController]
  [Route("api/bookmarktags")]
  public class BookmarkTagsController : LoggedController<BookmarkTagsController>
  {
    /// <summary>
    /// The BookmarkTag service.
    /// </summary>
    private IBookmarkTagService _bookmarkTagService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookmarksController"/> class.
    /// </summary>
    /// <param name="logger">The logger used.</param>
    /// <param name="bookmarkTagService">The <see cref="IBookmarkService"/> used.</param>
    public BookmarkTagsController(ILogger<BookmarkTagsController> logger, IBookmarkTagService bookmarkTagService) : base(logger)
    {
      _bookmarkTagService = bookmarkTagService;
    }

    // GET: api/BookmarkTags
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(_bookmarkTagService.Get());
    }

    // GET: api/BookmarkTags/5
    [Authorize]
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
      return Ok(_bookmarkTagService.Get(id));
    }

    // POST: api/BookmarkTags
    [Authorize]
    [HttpPost]
    public IActionResult Post([FromBody] BookmarkTag bookmarkTag)
    {
      try
      {
        return Created(string.Empty, _bookmarkTagService.Create(bookmarkTag));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // PUT: api/BookmarkTags/5
    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Put(string id, [FromBody] BookmarkTag bookmarkTag)
    {
      try
      {
        return Ok(_bookmarkTagService.Update(id, bookmarkTag));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // PUT: api/BookmarkTags
    [Authorize]
    [HttpPut()]
    public IActionResult Put([FromBody] BookmarkTag bookmarkTag)
    {
      try
      {
        return Ok(_bookmarkTagService.Update(bookmarkTag.Id, bookmarkTag));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // DELETE: api/BookmarkTags/5
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
      try
      {
        return Ok(_bookmarkTagService.Delete(id));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // DELETE: api/BookmarkTags
    [Authorize]
    [HttpDelete()]
    public IActionResult Delete([FromBody] BookmarkTag bookmarkTag)
    {
      try
      {
        return Ok(_bookmarkTagService.Delete(bookmarkTag));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
  }
}
