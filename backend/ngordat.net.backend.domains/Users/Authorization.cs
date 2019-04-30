namespace ngordat.net.backend.domains.Users
{
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;
  using ngordat.net.backend.domains.Mixins;
  using System.Collections.Generic;

  /// <summary>
  /// Authorization class.
  /// </summary>
  public class Authorization : TracedObject
  {
    /// <summary>
    /// Gets or sets the Id of the <see cref="Authorization"/>.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the Name of the <see cref="Authorization"/>.
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the Name of the resource that this <see cref="Authorization"/> gives access to.
    /// </summary>
    [BsonElement("ressourcename")]
    public string ResourceName { get; set; }

    /// <summary>
    /// Gets or sets the Rights of the <see cref="Authorization"/>.
    /// </summary>
    [BsonElement("rights")]
    public Rights Rigths { get; set; }

    /// <summary>
    /// Gets or sets Additional rights associated with the <see cref="Authorization"/>.
    /// </summary>
    [BsonElement("additionalrigths")]
    public IEnumerable<KeyValuePair<string, bool>> AdditionalRights { get; set; }
  }
}
