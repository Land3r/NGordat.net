namespace ngordat.net.backend.services.Users
{
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.Logging;
  using MongoDB.Driver;
  using ngordat.net.backend.domains;
  using System.Collections.Generic;

  /// <summary>
  /// UserGroupService class.
  /// </summary>
  public class UserGroupService : LoggedService<UserGroupService>, IUserGroupService
  {
    /// <summary>
    /// The collection of <see cref="Authorization"/> from the database.
    /// </summary>
    private readonly IMongoCollection<UserGroup> _userGroups;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizationService"/> class.
    /// </summary>
    /// <param name="config">The configuration used.</param>
    /// <param name="logger">The logger used.</param>
    public UserGroupService(ILogger<UserGroupService> logger, IConfiguration config) : base(logger)
    {
      MongoClient client = new MongoClient(config.GetConnectionString("NgordatnetDb"));
      IMongoDatabase database = client.GetDatabase("NgordatnetDb");
      _userGroups = database.GetCollection<UserGroup>("UserGroups");
    }

    #region CRUD

    /// <summary>
    /// Gets the list of all <see cref="UserGroup"/>.
    /// </summary>
    /// <returns>The list of <see cref="UserGroup"/>.</returns>
    public IEnumerable<UserGroup> Get()
    {
      return _userGroups.Find<UserGroup>(userGroup => true).ToList();
    }

    /// <summary>
    /// Gets a <see cref="UserGroup"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="UserGroup"/>.</param>
    /// <returns>The <see cref="UserGroup"/> that match the Id.</returns>
    public UserGroup Get(string id)
    {
      return _userGroups.Find<UserGroup>(userGroup => userGroup.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Creates a new <see cref="UserGroup"/>.
    /// </summary>
    /// <param name="userGroup">The <see cref="UserGroup"/> to insert in database.</param>
    /// <returns>The <see cref="UserGroup"/> created.</returns>
    public UserGroup Create(UserGroup authorization)
    {
      _userGroups.InsertOne(authorization);
      return authorization;
    }

    /// <summary>
    /// Updates a <see cref="UserGroup"/>.
    /// </summary>
    /// <param name="userGroupIn">The updated <see cref="UserGroup"/>.</param>
    /// <returns>The <see cref="UserGroup"/> updated.</returns>
    public UserGroup Update(UserGroup userGroupIn)
    {
      _userGroups.ReplaceOne(userGroup => userGroup.Id == userGroup.Id, userGroupIn);
      return userGroupIn;
    }

    /// <summary>
    /// Updates a <see cref="UserGroup"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="UserGroup"/> to update.</param>
    /// <param name="userGroupIn">The updated <see cref="UserGroup"/>.</param>
    /// <returns>The <see cref="UserGroup"/> updated.</returns>
    public UserGroup Update(string id, UserGroup userGroupIn)
    {
      _userGroups.ReplaceOne(userGroup => userGroup.Id == id, userGroupIn);
      return userGroupIn;
    }

    /// <summary>
    /// Deletes a <see cref="UserGroup"/>.
    /// </summary>
    /// <param name="userGroupIn">The <see cref="UserGroup"/> to delete.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    public bool Delete(UserGroup userGroupIn)
    {
      _userGroups.DeleteOne(userGroup => userGroup.Id == userGroupIn.Id);
      return true;
    }

    /// <summary>
    /// Deletes a <see cref="UserGroup"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="UserGroup"/> to delete.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    public bool Delete(string id)
    {
      _userGroups.DeleteOne(userGroup => userGroup.Id == id);
      return true;
    }

    #endregion CRUD
  }
}
