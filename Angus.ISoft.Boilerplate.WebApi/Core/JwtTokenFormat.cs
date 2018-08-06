using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;

namespace Angus.ISoft.Boilerplate.WebApi.Core
{
    /// <summary>
    /// 自定义 jwt token 的格式 
    /// </summary>
    public class JwtTokenFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly byte[] _secret;
        private readonly string _issuer;
        public string SignatureAlgorithm
        {
            get { return "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"; }
        }

        public string DigestAlgorithm
        {
            get { return "http://www.w3.org/2001/04/xmlenc#sha256"; }
        }

        public JwtTokenFormat(string issuer, byte[] secret)
        {
            _secret = secret;
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var signingKey = new SigningCredentials(
                new InMemorySymmetricSecurityKey(_secret),
                                        SignatureAlgorithm,
                                        DigestAlgorithm);
            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            return new JwtSecurityTokenHandler().WriteToken(
                new JwtSecurityToken(_issuer, "Any", data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey));
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}