using System;
using System.Collections.Generic;
using System.Text;

namespace NeroliTech.Shared
{
    public class UserWithToken : User
    {

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(User user)
        {
            this.UserId = user.UserId;
            this.Username = user.Username;
            this.FirstName = user.FirstName;            
            this.LastName = user.LastName;            

            this.Role = user.Role;
        }
    }
}
