using ngordat.net.backend.domains.Users;

namespace ngordat.net.backend.api.Services
{
  public interface IAuthService
  {
    User Authenticate(string username, string password);
  }
}
