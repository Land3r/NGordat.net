namespace ngordat.net.backend.domains.Bookmarks
{
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;
  using ngordat.net.backend.domains.Mixins;
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// A bookmark.
  /// </summary>
  public class Bookmark : TracedObject
  {
    /// <summary>
    /// Gets or sets the Id for the <see cref="Bookmark"/>.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the Name for the <see cref="Bookmark"/>.
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the Uri for the <see cref="Bookmark"/>.
    /// </summary>
    [BsonElement("uri")]
    public Uri Uri { get; set; }

    /// <summary>
    /// Gets or sets a list of id of <see cref="BookmarkTag"/> used by this <see cref="Bookmark"/>.
    /// </summary>
    [BsonElement("bookmarks_id")]
    public IEnumerable<string> Tags_id { get; set; }
  }
}
