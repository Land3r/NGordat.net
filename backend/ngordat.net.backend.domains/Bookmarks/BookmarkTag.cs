namespace ngordat.net.backend.domains.Bookmarks
{
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;
  using ngordat.net.backend.domains.Mixins;
  using System.Collections.Generic;

  /// <summary>
  /// A tag used to classify <see cref="Bookmark"/>.
  /// </summary>
  public class BookmarkTag : TracedObject
  {
    /// <summary>
    /// Gets or sets the Id of the <see cref="BookmarkTag"/>.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the Name of the <see cref="BookmarkTag"/>.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the Font color used for this <see cref="BookmarkTag"/>.
    /// </summary>
    public string FontColor { get; set; }

    /// <summary>
    /// Gets or sets the color for this <see cref="BookmarkTag"/>.
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// Gets or sets the Icon for this <see cref="BookmarkTag"/>.
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// Gets or sets a list of Id of sub tags, that belong to this <see cref="BookmarkTag"/>.
    /// </summary>
    public IEnumerable<string> ChildrenTags_id { get; set; }
  }
}
