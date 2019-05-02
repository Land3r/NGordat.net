namespace ngordat.net.backend.services.Users
{
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.Logging;
  using MongoDB.Driver;
  using ngordat.net.backend.domains.Users;
  using System.Collections.Generic;

  /// <summary>
  /// AuthorizationService class.
  /// </summary>
  public class AuthorizationService : LoggedService<AuthorizationService>, IAuthorizationService
  {
    /// <summary>
    /// The collection of <see cref="Authorization"/> from the database.
    /// </summary>
    private readonly IMongoCollection<Authorization> _authorizations;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizationService"/> class.
    /// </summary>
    /// <param name="config">The configuration used.</param>
    /// <param name="logger">The logger used.</param>
    public AuthorizationService(ILogger<AuthorizationService> logger, IConfiguration config) : base(logger)
    {
      MongoClient client = new MongoClient(config.GetConnectionString("NgordatnetDb"));
      IMongoDatabase database = client.GetDatabase("NgordatnetDb");
      _authorizations = database.GetCollection<Authorization>("Authorizations");
    }

    #region CRUD

    /// <summary>
    /// Gets the list of all <see cref="Authorization"/>.
    /// </summary>
    /// <returns>The list of <see cref="Authorization"/>.</returns>
    public IEnumerable<Authorization> Get()
    {
      return _authorizations.Find<Authorization>(authorization => true).ToList();
    }

    /// <summary>
    /// Gets a <see cref="Authorization"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="Authorization"/>.</param>
    /// <returns>The <see cref="Authorization"/> that match the Id.</returns>
    public Authorization Get(string id)
    {
      return _authorizations.Find<Authorization>(authorization => authorization.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Creates a new <see cref="Authorization"/>.
    /// </summary>
    /// <param name="authorization">The <see cref="Authorization"/> to insert in database.</param>
    /// <returns>The <see cref="Authorization"/> created.</returns>
    public Authorization Create(Authorization authorization)
    {
      _authorizations.InsertOne(authorization);
      return authorization;
    }

    /// <summary>
    /// Updates a <see cref="Authorization"/>.
    /// </summary>
    /// <param name="authorizationIn">The updated <see cref="Authorization"/>.</param>
    /// <returns>The <see cref="Authorization"/> updated.</returns>
    public Authorization Update(Authorization authorizationIn)
    {
      _authorizations.ReplaceOne(user => user.Id == authorizationIn.Id, authorizationIn);
      return authorizationIn;
    }

    /// <summary>
    /// Updates a <see cref="Authorization"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="Authorization"/> to update.</param>
    /// <param name="authorizationIn">The updated <see cref="Authorization"/>.</param>
    /// <returns>The <see cref="Authorization"/> updated.</returns>
    public Authorization Update(string id, Authorization authorizationIn)
    {
      _authorizations.ReplaceOne(authorization => authorization.Id == id, authorizationIn);
      return authorizationIn;
    }

    /// <summary>
    /// Deletes a <see cref="Authorization"/>.
    /// </summary>
    /// <param name="userIn">The <see cref="Authorization"/> to delete.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    public bool Delete(Authorization authorizationIn)
    {
      _authorizations.DeleteOne(authorization => authorization.Id == authorizationIn.Id);
      return true;
    }

    /// <summary>
    /// Deletes a <see cref="Authorization"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="Authorization"/> to delete.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    public bool Delete(string id)
    {
      _authorizations.DeleteOne(authorization => authorization.Id == id);
      return true;
    }

    #endregion CRUD
  }
}
