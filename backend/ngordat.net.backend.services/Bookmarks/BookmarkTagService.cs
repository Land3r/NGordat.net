namespace ngordat.net.backend.services.Bookmarks
{
  using Microsoft.Extensions.Configuration;
  using MongoDB.Driver;
  using ngordat.net.backend.domains.Bookmarks;
  using System.Collections.Generic;

  /// <summary>
  /// BookmarkTagService class.
  /// </summary>
  public class BookmarkTagService : IBookmarkTagService
  {
    /// <summary>
    /// The collection of <see cref="BookmarkTag"/> from the database.
    /// </summary>
    private readonly IMongoCollection<BookmarkTag> _bookmarks;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookmarkTagService"/> class.
    /// </summary>
    /// <param name="config">The configuration used.</param>
    /// <pa
    public BookmarkTagService(IConfiguration config)
    {
      MongoClient client = new MongoClient(config.GetConnectionString("NgordatnetDb"));
      IMongoDatabase database = client.GetDatabase("NgordatnetDb");
      _bookmarks = database.GetCollection<BookmarkTag>("BookmarkTags");
    }

    #region CRUD

    /// <summary>
    /// Gets the list of all <see cref="BookmarkTag"/>.
    /// </summary>
    /// <returns>The list of <see cref="BookmarkTag"/>.</returns>
    public IEnumerable<BookmarkTag> Get()
    {
      return _bookmarks.Find<BookmarkTag>(bookmark => true).ToList();
    }

    /// <summary>
    /// Gets a <see cref="BookmarkTag"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="BookmarkTag"/>.</param>
    /// <returns>The <see cref="BookmarkTag"/> that match the Id.</returns>
    public BookmarkTag Get(string id)
    {
      return _bookmarks.Find<BookmarkTag>(bookmarkTag => bookmarkTag.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Creates a new <see cref="BookmarkTag"/>.
    /// </summary>
    /// <param name="bookmarkTag">The <see cref="BookmarkTag"/> to insert in database.</param>
    /// <returns>The <see cref="BookmarkTag"/> created.</returns>
    public BookmarkTag Create(BookmarkTag bookmarkTag)
    {
      _bookmarks.InsertOne(bookmarkTag);
      return bookmarkTag;
    }

    /// <summary>
    /// Updates a <see cref="BookmarkTag"/>.
    /// </summary>
    /// <param name="bookmarkTagIn">The updated <see cref="BookmarkTag"/>.</param>
    /// <returns>The <see cref="BookmarkTag"/> updated.</returns>
    public BookmarkTag Update(BookmarkTag bookmarkTagIn)
    {
      _bookmarks.ReplaceOne(bookmarkTag => bookmarkTag.Id == bookmarkTag.Id, bookmarkTagIn);
      return bookmarkTagIn;
    }

    /// <summary>
    /// Updates a <see cref="BookmarkTag"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="BookmarkTag"/> to update.</param>
    /// <param name="bookmarkTagIn">The updated <see cref="BookmarkTag"/>.</param>
    /// <returns>The <see cref="BookmarkTag"/> updated.</returns>
    public BookmarkTag Update(string id, BookmarkTag bookmarkTagIn)
    {
      _bookmarks.ReplaceOne(bookmark => bookmark.Id == id, bookmarkTagIn);
      return bookmarkTagIn;
    }

    /// <summary>
    /// Deletes a <see cref="BookmarkTag"/>.
    /// </summary>
    /// <param name="bookmarkTagIn">The <see cref="BookmarkTag"/> to delete.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    public bool Delete(BookmarkTag bookmarkTagIn)
    {
      _bookmarks.DeleteOne(bookmarkTag => bookmarkTag.Id == bookmarkTagIn.Id);
      return true;
    }

    /// <summary>
    /// Deletes a <see cref="BookmarkTag"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="BookmarkTag"/> to delete.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    public bool Delete(string id)
    {
      _bookmarks.DeleteOne(bookmarkTag => bookmarkTag.Id == id);
      return true;
    }

    #endregion CRUD
  }
}
