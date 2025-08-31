﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Ecommerce.DataAccess.ApplicationContext;
using Ecommerce.Entities.Models.Auth.Identity;
using Ecommerce.Entities.Models.Auth.UserTokens;
using Ecommerce.Utilities.Configurations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.DataAccess.Services.Token
{
    public class TokenStoreService : ITokenStoreService
    {
        private readonly SymmetricSecurityKey _symmetricSecurityKey;
        private readonly UserManager<User> _userManager; // To get user roles 
        private readonly JwtSettings _jwtSettings;
        private readonly ApplicationDbContext _ApplicationDbContext;

        public TokenStoreService(IOptions<JwtSettings> jwtOptions, UserManager<User> userManager, ApplicationDbContext ApplicationDbContext)
        {
            _jwtSettings = jwtOptions.Value ?? throw new ArgumentNullException(nameof(jwtOptions));
            _userManager = userManager;
            if (string.IsNullOrEmpty(_jwtSettings.SigningKey))
            {
                throw new ArgumentException("JWT SigningKey is not configured.");
            }
            _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
            _ApplicationDbContext = ApplicationDbContext;
        }
        public async Task<string> CreateAccessTokenAsync(User appUser)
        {
            var roles = await _userManager.GetRolesAsync(appUser);
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.GivenName,appUser.UserName)
            };

            foreach (var role in roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(TokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Refresh Token Methods
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
        public async Task SaveRefreshTokenAsync(string userId, string refreshToken)
        {
            await _ApplicationDbContext.UserRefreshTokens.AddAsync(new UserRefreshToken
            {
                UserId = userId,
                Token = refreshToken,
                ExpiryDateUtc = DateTime.UtcNow.AddDays(7),
                IsUsed = false
            });

            await _ApplicationDbContext.SaveChangesAsync();
        }
        public async Task InvalidateOldTokensAsync(string userId)
        {
            var tokens = await _ApplicationDbContext.UserRefreshTokens
                .Where(r => r.UserId == userId)
                .ToListAsync();

            _ApplicationDbContext.UserRefreshTokens.RemoveRange(tokens);
            await _ApplicationDbContext.SaveChangesAsync();
        }
        public async Task<bool> IsValidAsync(string refreshToken)
        {
            return await _ApplicationDbContext.UserRefreshTokens
                .AnyAsync(r => r.Token == refreshToken && !r.IsUsed && r.ExpiryDateUtc > DateTime.UtcNow);
        }
        
        public async Task<(string AccessToken, string RefreshToken)> GenerateAndStoreTokensAsync(string userId, User user)
        {
                var accessToken = await CreateAccessTokenAsync(user);
                var refreshToken = GenerateRefreshToken();
                await SaveRefreshTokenAsync(userId, refreshToken);
            return (accessToken, refreshToken);   
        }
    }
}
