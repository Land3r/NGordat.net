namespace ngordat.net.backend.domains.Users
{
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;
  using System;

  /// <summary>
  /// A user of the application
  /// </summary>
  public class User
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    /// <summary>
    /// Gets or sets the id of the <see cref="User"/>.
    /// </summary>
    public string Id { get; set; }

    [BsonElement("firstname")]
    /// <summary>
    /// Gets or sets the firstname of the <see cref="User"/>.
    /// </summary>
    public string Firstname { get; set; }

    [BsonElement("lastname")]
    /// <summary>
    /// Gets or sets the lastname of the <see cref="User"/>.
    /// </summary>
    public string Lastname { get; set; }

    [BsonElement("username")]
    /// <summary>
    /// Gets or sets the username of the <see cref="User"/>.
    /// </summary>
    public string Username { get; set; }

    [BsonElement("email")]
    /// <summary>
    /// Gets or sets the email of the <see cref="User"/>
    /// </summary>
    public string Email { get; set; }

    [BsonElement("password")]
    /// <summary>
    /// Gets or sets the password of the <see cref="User"/>.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the token of the <see cref="User"/>.
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Gets or sets the date of creation of the <see cref="User"/>.
    /// </summary>
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets whether or not the <see cref="User"/> went through the email verification process.
    /// </summary>
    [BsonElement("verified")]
    public bool Verified { get; set; }
  }
}
