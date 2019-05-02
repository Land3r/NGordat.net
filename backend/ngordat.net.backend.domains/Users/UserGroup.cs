namespace ngordat.net.backend.domains
{
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;
  using System.Collections.Generic;

  /// <summary>
  /// A group comprised of <see cref="Users"/>, with specific Authorizations attached to it.
  /// </summary>
  public class UserGroup : TracedObject
  {
    /// <summary>
    /// Gets or sets the Id of the <see cref="UserGroup"/>.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the <see cref="UserGroup"/>.
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the list of <see cref="Authorization"/> associated with this group.
    /// </summary>
    [BsonElement("authorizations_id")]
    public IEnumerable<string> Authorizations_id { get; set; }

    /// <summary>
    /// Gets or sets the list of <see cref="User"/> that are part of this group.
    /// </summary>
    [BsonElement("users_id")]
    public IEnumerable<string> Users_id { get; set; }
  }
}
