using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeroliTech.Shared;
using NeroliTech.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace NeroliTech.Server.Handlers
{
    //public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    //{
    //    private readonly NeroliDBContext _dbContext;

    //    public BasicAuthenticationHandler(
    //        IOptionsMonitor<AuthenticationSchemeOptions> options,
    //        ILoggerFactory logger, 
    //        UrlEncoder encoder, 
    //        ISystemClock clock,
    //        NeroliDBContext dbContext) : base(options, logger, encoder, clock)
    //    {
    //        _dbContext = dbContext;
    //    }

    //    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    //    {
    //        if (!Request.Headers.ContainsKey("Authorization"))
    //            return AuthenticateResult.Fail("Authorization header was not found");

    //        try
    //        {
    //            var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
    //            var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
    //            string[] credentials = Encoding.UTF8.GetString(bytes).Split(":");
    //            string userName = credentials[0];
    //            string password = credentials[1];

    //            User user = _dbContext.Users.Where(u => u.Username == userName && u.Password == password).FirstOrDefault();

    //            if (user == null)
    //                return AuthenticateResult.Fail("Invalid username or password");
    //            {
    //                var claims = new[] { new Claim(ClaimTypes.Name, user.Username) };
    //                var identity = new ClaimsIdentity(claims, Scheme.Name);
    //                var principal = new ClaimsPrincipal(identity);
    //                var ticket = new AuthenticationTicket(principal, Scheme.Name);

    //                return AuthenticateResult.Success(ticket);
    //            }

    //        }
    //        catch (Exception)
    //        {
    //            return AuthenticateResult.Fail("Error has occured");
    //        }
    //    }
    //}
}
