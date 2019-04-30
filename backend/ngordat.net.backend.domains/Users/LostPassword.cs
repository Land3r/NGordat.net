namespace ngordat.net.backend.domains.Users
{
  /// <summary>
  /// A Password lost request object.
  /// </summary>
  public class LostPassword
  {
    /// <summary>
    /// The username of the <see cref="User"/> requesting the password reset.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// The email of the <see cref="User"/> requesting the password reset.
    /// </summary>
    public string Email { get; set; }
  }
}
