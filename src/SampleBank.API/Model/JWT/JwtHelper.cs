using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SampleBank.Core.Entity;
using Claim = SampleBank.Core.Entity.Claim;

namespace SampleBank.API.Model.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private IConfiguration Configuration { get; } 
        private TokenOptions TokenOptions { get; }
        private readonly DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            TokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _accessTokenExpiration = DateTime.Now.AddMinutes(TokenOptions.AccessTokenExpiration);
        }

        public AccessToken CreateToken(User user, List<Claim> operationClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenOptions.SecurityKey));
            var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwt = new JwtSecurityToken(
                issuer: TokenOptions.Issuer,
                audience: TokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredential
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new AccessToken()
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        private IEnumerable<System.Security.Claims.Claim> SetClaims(User user, List<Claim> userClaims)
        {
            var claims = new List<System.Security.Claims.Claim>
            {
                new(JwtRegisteredClaimNames.UniqueName, user.Username),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, $"{user.Name} {user.Surname}")
            };
            userClaims?.Select(x => x.Name).ToList().ForEach(role => claims.Add(new System.Security.Claims.Claim(ClaimTypes.Role, role)));
            return claims;
        }
    }
}