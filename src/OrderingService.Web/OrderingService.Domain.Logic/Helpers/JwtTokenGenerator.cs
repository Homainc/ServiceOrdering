using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Domain.Logic.Helpers
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly AppSettings _appSettings;
        public JwtTokenGenerator(IOptions<AppSettings> appSettings) => _appSettings = appSettings.Value;
        public string GenerateUserToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var userToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(userToken);
        }
    }
}
