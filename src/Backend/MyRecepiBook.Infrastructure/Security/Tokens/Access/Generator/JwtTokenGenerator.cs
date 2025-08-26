using Microsoft.IdentityModel.Tokens;
using MyRecipeBook.Domain.Security.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace MyRecipeBook.Infrastructure.Security.Tokens.Access.Generator
{
    public class JwtTokenGenerator : IAccessTokenGenerator
    {
        private readonly uint _experationTimeMinutes;
        private readonly string _signingKey;

        public JwtTokenGenerator(uint experationTimeMinutes, string signingKey)
        {
            _experationTimeMinutes = experationTimeMinutes;
            _signingKey = signingKey;
        }

        public string Generate(Guid userIdentifier)
        {
            var claims = new List<Claim>();
            {
                new Claim(ClaimTypes.Sid, userIdentifier.ToString());
            };

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_experationTimeMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var secutiryToken = tokenHandler.CreateToken(tokenDescripter);

            return tokenHandler.WriteToken(secutiryToken);
        }

        private SymmetricSecurityKey SecurityKey()
        {
            var bytes = Encoding.UTF8.GetBytes(_signingKey);
            return new SymmetricSecurityKey(bytes);
        }
    }
}
