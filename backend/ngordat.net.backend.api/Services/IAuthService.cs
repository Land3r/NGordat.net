using ngordat.net.backend.domains.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ngordat.net.backend.api.Services
{
    public interface IAuthService
    {
        User Authenticate(string username, string password);
    }
}
