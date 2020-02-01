using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alexander_neumann.api.Auth
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
    }
}
