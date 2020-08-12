using System;
using System.Collections.Generic;

namespace NeroliTech.Server.Models
{
    public partial class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
