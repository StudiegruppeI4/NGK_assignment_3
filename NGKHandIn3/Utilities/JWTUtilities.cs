using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NGKHandIn3.Utilities
{
    public class JWTUtilities
    {
        public static string GenerateToken(string firstName, string lastName, string email, int userId)
        {
            var claims = new Claim[]
            {
                new Claim("UserId", userId.ToString()),
                new Claim("FirstName", firstName),
                new Claim("LastName", lastName),
                new Claim("Email", email),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(3)).ToUnixTimeSeconds().ToString()),
            };

            var key = Encoding.ASCII.GetBytes("The secret that needs to be at least 16 characters long for HmacSha256 and thats just how it is");
            var token = new JwtSecurityToken(
                issuer: "https://localhost:44309/",
                audience: "https://localhost:44309/",
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
