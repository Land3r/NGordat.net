namespace ngordat.net.backend.services.Users
{
  using System;
  using System.Collections.Generic;
  using System.IdentityModel.Tokens.Jwt;
  using System.Linq;
  using System.Text;
  using Microsoft.Extensions.Options;
  using ngordat.net.backend.domains.Users;

  public class UserService : IUserService
  {
    public UserService()
    {
    }

    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    private List<User> _users = new List<User>
    {
      new User { Id = 1, Firstname = "Nicolas", Lastname = "Gordat", Username = "NGordat", Password = "password" }
    };



        public User Get(int id)
        {
            return _users.SingleOrDefault(x => x.Id == id);
        }
        
        /// <summary>
        /// Gets a sanitized user.
        /// It will remove the password of the user.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to sanitize.</param>
        /// <returns>The <see cref="User"/> sanitized.</returns>
        public static User GetSanitized(User user)
        {
            User sanitizedUser = new User()
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                Token = user.Token,
                Password = null
            };

            return sanitizedUser;
        }

        public User Get(string username)
        {
            return _users.SingleOrDefault(x => x.Username.ToUpperInvariant() == username.ToUpperInvariant());
        }

        public User Get(string username, string password)
        {
            return _users.SingleOrDefault(x => x.Username.ToUpperInvariant() == username.ToUpperInvariant() && x.Password == password);
        }
    }
}
