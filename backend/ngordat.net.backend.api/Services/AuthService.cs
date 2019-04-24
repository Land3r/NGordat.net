using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ngordat.net.backend.api.Settings;
using ngordat.net.backend.domains.Users;
using ngordat.net.backend.services.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ngordat.net.backend.api.Services
{
    public class AuthService : IAuthService
    {
        private IUserService _userService;

        private AppSettings _appSettings;

        public AuthService(IOptions<AppSettings> appSettings,
            IUserService userService)
        {
            _appSettings = appSettings.Value;
            _userService = userService;
        }

        public User Authenticate(string username, string password)
        {
            var user = _userService.Get(username, password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

    }
}
