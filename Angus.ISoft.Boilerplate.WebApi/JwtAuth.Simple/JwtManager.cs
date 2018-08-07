using JWT;
using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angus.ISoft.Boilerplate.WebApi.JwtAuth.Simple
{
    public class JwtManager
    {
        private const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

        public static string GenerateToken(string userId, string userName)
        {
            var token = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(secret)
                .AddClaim("userid", userId)
                .AddClaim("username", userName)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                .Build();

            return token;
        }

        public static bool ValidateToken(string token, string userId)
        {
            var payload = new JwtBuilder()
                .WithSecret(secret)
                .MustVerifySignature()
                .Decode<IDictionary<string, string>>(token);

            var _userId = payload["userid"];
            var _username = payload["username"];
            if (userId == _userId) return true;
            return false;
        }
    }
}