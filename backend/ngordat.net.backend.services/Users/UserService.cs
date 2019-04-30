namespace ngordat.net.backend.services.Users
{
  using Microsoft.AspNetCore.Cryptography.KeyDerivation;
  using Microsoft.Extensions.Configuration;
  using MongoDB.Driver;
  using ngordat.net.backend.domains.Users;
  using ngordat.net.backend.transversal.Settings;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  /// <summary>
  /// UserService class.
  /// </summary>
  public class UserService : IUserService
  {
    /// <summary>
    /// The collection of <see cref="User"/> from the database.
    /// </summary>
    private readonly IMongoCollection<User> _users;

    /// <summary>
    /// The salt used for the password.
    /// </summary>
    private string _passwordSalt;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="config">The configuration used.</param>
    /// <param name="appSettings">The appSettings used.</param>
    public UserService(IConfiguration config, IAppSettings appSettings)
    {
      MongoClient client = new MongoClient(config.GetConnectionString("NgordatnetDb"));
      IMongoDatabase database = client.GetDatabase("NgordatnetDb");
      _users = database.GetCollection<User>("Users");

      _passwordSalt = appSettings.PasswordSalt;
    }

    #region CRUD

    /// <summary>
    /// Gets the list of all <see cref="User"/>.
    /// </summary>
    /// <returns>The list of <see cref="User"/>.</returns>
    public IEnumerable<User> Get()
    {
      return GetSanitized(_users.Find<User>(user => true).ToList());
    }

    /// <summary>
    /// Gets a <see cref="User"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="User"/>.</param>
    /// <returns>The <see cref="User"/> that match the Id.</returns>
    public User Get(string id)
    {
      return GetSanitized(_users.Find<User>(user => user.Id == id).FirstOrDefault());
    }

    /// <summary>
    /// Creates a new <see cref="User"/>.
    /// </summary>
    /// <param name="user">The <see cref="User"/> to insert in database.</param>
    /// <returns>The <see cref="User"/> created.</returns>
    public User Create(User user)
    {
      string hashedPassword = GetHash(user.Password);
      user.Password = hashedPassword;
      _users.InsertOne(user);
      return GetSanitized(user);
    }

    /// <summary>
    /// Updates a <see cref="User"/>.
    /// </summary>
    /// <param name="userIn">The updated <see cref="User"/>.</param>
    /// <returns>The <see cref="User"/> updated.</returns>
    public User Update(User userIn)
    {
      _users.ReplaceOne(user => user.Id == userIn.Id, userIn);
      return GetSanitized(userIn);
    }

    /// <summary>
    /// Updates a <see cref="User"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="User"/> to update.</param>
    /// <param name="userIn">The updated <see cref="User"/>.</param>
    /// <returns>The <see cref="User"/> updated.</returns>
    public User Update(string id, User userIn)
    {
      _users.ReplaceOne(user => user.Id == id, userIn);
      return GetSanitized(userIn);
    }

    /// <summary>
    /// Deletes a <see cref="User"/>.
    /// </summary>
    /// <param name="userIn">The <see cref="User"/> to delete.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    public bool Delete(User userIn)
    {
      _users.DeleteOne(user => user.Id == userIn.Id);
      return true;
    }

    /// <summary>
    /// Deletes a <see cref="User"/>, based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="User"/> to delete.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    public bool Delete(string id)
    {
      _users.DeleteOne(user => user.Id == id);
      return true;
    }

    #endregion CRUD

    #region Utilities

    /// <summary>
    /// Gets a sanitized user.
    /// It will remove the password of the user.
    /// </summary>
    /// <param name="user">The <see cref="User"/> to sanitize.</param>
    /// <returns>The <see cref="User"/> sanitized.</returns>
    public static User GetSanitized(User user)
    {
      if (user != null)
      {
        User sanitizedUser = new User()
        {
          Id = user.Id,
          Firstname = user.Firstname,
          Lastname = user.Lastname,
          Username = user.Username,
          Email = user.Email,
          Token = user.Token,
          Password = null
        };

        return sanitizedUser;
      }
      else
      {
        return null;
      }
    }
    /// <summary>
    /// Gets a list of sanitized <see cref="User"/>.
    /// It will remove the password of the users.
    /// </summary>
    /// <param name="user">The list of <see cref="User"/> to sanitize.</param>
    /// <returns>The list of <see cref="User"/> sanitized.</returns>
    public static IEnumerable<User> GetSanitized(IEnumerable<User> users)
    {
      foreach (User user in users)
      {
        yield return GetSanitized(user);
      }
    }

    /// <summary>
    /// Gets the hash (with salt) version of a clear text password.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <returns>The salted and hashed password.</returns>
    public string GetHash(string password)
    {
      var saltedPasswordBytes = KeyDerivation.Pbkdf2(
        password: password,
        salt: Encoding.ASCII.GetBytes(_passwordSalt),
        prf: KeyDerivationPrf.HMACSHA512,
        iterationCount: 10000,
        numBytesRequested: 256 / 8
      );
      return Convert.ToBase64String(saltedPasswordBytes);
    }

    #endregion Utilities

    /// <summary>
    /// Gets the <see cref="User"/> with the provided username and password.
    /// </summary>
    /// <param name="username">The username of the <see cref="User"/>.</param>
    /// <param name="password">The password of the <see cref="User"/>.</param>
    /// <returns></returns>
    public User Get(string username, string password)
    {
      string hashedPassword = GetHash(password);
      return GetSanitized(_users.Find(user => user.Username.ToUpperInvariant() == username.ToUpperInvariant() && user.Password == hashedPassword).FirstOrDefault());
    }
  }
}
