using ngordat.net.backend.domains.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ngordat.net.backend.services.Users
{
    public interface IUserService
    {
        User Get(int id);

        User Get(string username);

        User Get(string username, string password);
    }
}
