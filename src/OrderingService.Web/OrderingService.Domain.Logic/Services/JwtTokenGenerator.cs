using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrderingService.Data.Code.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code.Configs;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly AppSettings _appSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ISaveProvider _saveProvider;

        public JwtTokenGenerator(IOptions<AppSettings> appSettings, IRefreshTokenRepository refreshTokenRepository, ISaveProvider saveProvider)
        {
            _appSettings = appSettings.Value;
            _refreshTokenRepository = refreshTokenRepository;
            _saveProvider = saveProvider;
        }

        private static IEnumerable<Claim> CreateUserClaims(User user)
        {
            return new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name),
            };
        }

        public async Task<AccessTokenDto> CreateAccessTokenAsync(User user)
        {
            var jwtToken = GenerateJwtToken(CreateUserClaims(user));
            var refreshToken =new RefreshToken
            {
                UserId = user.Id,
                Token = GenerateRefreshToken()
            };

            if(await _refreshTokenRepository.AnyByUserIdAsync(user.Id))
                _refreshTokenRepository.Update(refreshToken);
            else
                _refreshTokenRepository.Create(refreshToken);

            await _saveProvider.SaveAsync();

            return new AccessTokenDto {RefreshToken = refreshToken.Token, Token = jwtToken};
        }

        private string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var userToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(userToken);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public async Task<AccessTokenDto> RefreshAccessTokenAsync(AccessTokenDto accessToken)
        {
            var principal = GetPrincipalFromExpiredToken(accessToken.Token);
            var userId = new Guid(principal.Identity.Name);
            var savedRefreshToken = await _refreshTokenRepository.GetByUserIdOrDefaultAsync(userId);
            if(savedRefreshToken.Token != accessToken.RefreshToken)
                throw new SecurityTokenException($"Invalid refresh token for user with id {userId}");

            var newJwtToken = GenerateJwtToken(principal.Claims);
            var newRefreshToken = GenerateRefreshToken();

            _refreshTokenRepository.Delete(savedRefreshToken);
            _refreshTokenRepository.Create(new RefreshToken {Token = newRefreshToken, UserId = userId});
            await _saveProvider.SaveAsync();

            return new AccessTokenDto {RefreshToken = newRefreshToken, Token = newJwtToken};
        } 
    }
}
