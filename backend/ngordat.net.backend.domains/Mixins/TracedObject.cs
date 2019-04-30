namespace ngordat.net.backend.domains.Mixins
{
  using MongoDB.Bson.Serialization.Attributes;
  using System;

  /// <summary>
  /// Represents an object that is traced.
  /// </summary>
  public abstract class TracedObject
  {
    /// <summary>
    /// Gets or sets the id of the <see cref="User"/> that created the <see cref="TracedObject"/>.
    /// </summary>
    [BsonElement("createdBy_id")]
    public string CreatedBy_id { get; set; }

    /// <summary>
    /// Gets or sets the time of creation of the <see cref="TracedObject"/>.
    /// </summary>
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the id of the <see cref="User"/> that modified the <see cref="TracedObject"/>.
    /// </summary>
    [BsonElement("modifiedBy_id")]
    public string ModifiedBy_id { get; set; }

    /// <summary>
    /// Gets or sets the time of modification of the <see cref="TracedObject"/>.
    /// </summary>
    [BsonElement("modifiedAt")]
    public DateTime ModifiedAt { get; set; }
  }
}
