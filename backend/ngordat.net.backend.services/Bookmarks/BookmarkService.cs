namespace ngordat.net.backend.services.Bookmarks
{
  using Microsoft.Extensions.Configuration;
  using MongoDB.Driver;
  using ngordat.net.backend.domains.Bookmarks;
  using System.Collections.Generic;

  /// <summary>
  /// BookmarkService class.
  /// </summary>
  public class BookmarkService : IBookmarkService
  {
    /// <summary>
    /// The collection of <see cref="Bookmark"/> from the database.
    /// </summary>
    private readonly IMongoCollection<Bookmark> _bookmarks;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookmarkService"/> class.
    /// </summary>
    /// <param name="config">The configuration used.</param>
    /// <pa
    public BookmarkService(IConfiguration config)
    {
      MongoClient client = new MongoClient(config.GetConnectionString("NgordatnetDb"));
      IMongoDatabase database = client.GetDatabase("NgordatnetDb");
      _bookmarks = database.GetCollection<Bookmark>("Bookmarks");
    }

    #region CRUD

    /// <summary>
    /// Gets the list of all <see cref="Bookmark"/>.
    /// </summary>
    /// <returns>The list of <see cref="Bookmark"/>.</returns>
    public IEnumerable<Bookmark> Get()
    {
      return _bookmarks.Find<Bookmark>(bookmark => true).ToList();
    }

    /// <summary>
    /// Gets a <see cref="Bookmark"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="Bookmark"/>.</param>
    /// <returns>The <see cref="Bookmark"/> that match the Id.</returns>
    public Bookmark Get(string id)
    {
      return _bookmarks.Find<Bookmark>(bookmark => bookmark.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Creates a new <see cref="Bookmark"/>.
    /// </summary>
    /// <param name="bookmark">The <see cref="Bookmark"/> to insert in database.</param>
    /// <returns>The <see cref="Bookmark"/> created.</returns>
    public Bookmark Create(Bookmark bookmark)
    {
      _bookmarks.InsertOne(bookmark);
      return bookmark;
    }

    /// <summary>
    /// Updates a <see cref="Bookmark"/>.
    /// </summary>
    /// <param name="bookmarkIn">The updated <see cref="Bookmark"/>.</param>
    /// <returns>The <see cref="Bookmark"/> updated.</returns>
    public Bookmark Update(Bookmark bookmarkIn)
    {
      _bookmarks.ReplaceOne(bookmark => bookmark.Id == bookmark.Id, bookmarkIn);
      return bookmarkIn;
    }

    /// <summary>
    /// Updates a <see cref="Bookmark"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="Bookmark"/> to update.</param>
    /// <param name="bookmarkIn">The updated <see cref="Bookmark"/>.</param>
    /// <returns>The <see cref="Bookmark"/> updated.</returns>
    public Bookmark Update(string id, Bookmark bookmarkIn)
    {
      _bookmarks.ReplaceOne(bookmark => bookmark.Id == id, bookmarkIn);
      return bookmarkIn;
    }

    /// <summary>
    /// Deletes a <see cref="Bookmark"/>.
    /// </summary>
    /// <param name="bookmarkIn">The <see cref="Bookmark"/> to delete.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    public bool Delete(Bookmark bookmarkIn)
    {
      _bookmarks.DeleteOne(bookmark => bookmark.Id == bookmarkIn.Id);
      return true;
    }

    /// <summary>
    /// Deletes a <see cref="Bookmark"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="Bookmark"/> to delete.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    public bool Delete(string id)
    {
      _bookmarks.DeleteOne(bookmark => bookmark.Id == id);
      return true;
    }

    #endregion CRUD
  }
}
