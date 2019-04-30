using MongoDB.Bson.Serialization.Attributes;

namespace ngordat.net.backend.domains.Users
{
  /// <summary>
  /// A set of rights, linked to a <see cref="Group"/>.
  /// </summary>
  public class Rights
  {
    /// <summary>
    /// Gets or sets whether or not the user is allowed to read items from the ressource.
    /// </summary>
    [BsonElement("canRead")]
    public bool CanRead { get; set; }

    /// <summary>
    /// Gets or sets whether or not the user is allowed to create items from the ressource.
    /// </summary>
    [BsonElement("canCreate")]
    public bool CanCreate { get; set; }

    /// <summary>
    /// Gets or sets whether or not the user is allowed to edit items from the ressource.
    /// </summary>
    [BsonElement("canEdit")]
    public bool CanEdit { get; set; }

    /// <summary>
    /// Gets or sets whether or not the user is allowed to delete items from the ressource.
    /// </summary>
    [BsonElement("canDelete")]
    public bool CanDelete { get; set; }
  }
}
