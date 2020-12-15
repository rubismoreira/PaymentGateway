using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CO.AcessControl.Core.Entities;
using CO.AcessControl.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CO.AcessControl.Service
{
    public class JwtHandlerService : IJwtHandlerService
    {
        private readonly IConfiguration _config;
        
        public JwtHandlerService(IConfiguration configuration)
        {
            this._config = configuration;
        }
        
        public string GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userInfo.Username),
                new Claim("id", userInfo.Id.ToString())
            };
            
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}