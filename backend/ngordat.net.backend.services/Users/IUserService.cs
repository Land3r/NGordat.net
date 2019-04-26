using ngordat.net.backend.domains.Users;

namespace ngordat.net.backend.services.Users
{
  public interface IUserService : ICrud<User>
  {
    /// <summary>
    /// Gets the <see cref="User"/> that match the provided username and password.
    /// </summary>
    /// <param name="username">The username of the <see cref="User"/>.</param>
    /// <param name="password">The password of the <see cref="User"/>.</param>
    /// <returns>The <see cref="User"/> that matches username and password.</returns>
    User Get(string username, string password);
  }
}
