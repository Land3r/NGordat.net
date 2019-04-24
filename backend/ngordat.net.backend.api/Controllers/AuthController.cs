using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ngordat.net.backend.api.Services;
using ngordat.net.backend.domains.Users;
using ngordat.net.backend.services.Users;

namespace ngordat.net.backend.api.Controllers
{
    /// <summary>
    /// Authentication controller api.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// The auth service.
        /// </summary>
        private IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="userService">The <see cref="IUserService">User service.</see></param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost()]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            User user = _authService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { error = "Username or password is incorrect." });

            return Ok(user);
        }
    }
}