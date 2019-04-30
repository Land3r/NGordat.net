namespace ngordat.net.backend.api.Services.Users
{
  using ngordat.net.backend.domains.Users;

  public interface IAuthService
  {
    User Authenticate(string username, string password);
  }
}
