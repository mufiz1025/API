using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace api.services
{
    public class TokenService :ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _config = config;
#pragma warning disable CS8604 // Possible null reference argument.
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
#pragma warning restore CS8604 // Possible null reference argument.

        }
       

        public string CreateToken(AppUser user)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email ),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName )
            };
#pragma warning restore CS8604 // Possible null reference argument.
            var creds = new SigningCredentials(_key , SecurityAlgorithms.HmacSha512Signature);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["Jwt : Issuer"],
                Audience = _config["Jwt : Audience"]
            };
            var TokenHandler = new JwtSecurityTokenHandler();
            var token = TokenHandler.CreateToken(TokenDescriptor);

            return TokenHandler.WriteToken(token);
        }
    }
}