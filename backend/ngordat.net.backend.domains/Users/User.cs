using System;
using System.Collections.Generic;
using System.Text;

namespace ngordat.net.backend.domains.Users
{
    /// <summary>
    /// A user of the application
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the id of the <see cref="User"/>.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the firstname of the <see cref="User"/>.
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the lastname of the <see cref="User"/>.
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Gets or sets the username of the <see cref="User"/>.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the <see cref="User"/>.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the token of the <see cref="User"/>.
        /// </summary>
        public string Token { get; set; }
    }
}
