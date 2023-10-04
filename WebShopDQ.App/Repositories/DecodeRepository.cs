using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories
{
    public class DecodeRepository : IDecodeRepository
    {
        private readonly IConfiguration _configuration;
        public DecodeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DecodeModel? Decode(string? token)
        {
            if (string.IsNullOrEmpty(token)) throw new KeyNotFoundException("Token not exist!");
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? ""));
            var TokenValidationParameters = new TokenValidationParameters()
            {
                // provide token
                ValidateIssuer = false,
                ValidateAudience = false,

                // sign in token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? "")),

                ClockSkew = TimeSpan.Zero,

                // Not check token expired
                ValidateLifetime = false
            };
            try
            {
                //check 1: AccessToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(
                    token, TokenValidationParameters, out var validatedToken);
                var infoDecode = new DecodeModel
                {
                    UserId = tokenInVerification.FindFirst("UserId")?.Value
                };
                return infoDecode;
            }
            catch
            {
                return null;
            }
        }
    }
}
