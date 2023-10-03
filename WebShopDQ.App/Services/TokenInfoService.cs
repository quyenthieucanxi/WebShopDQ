using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common.Exceptions;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services
{
    public class TokenInfoService : ITokenInfoService
    {
        private readonly IDecodeRepository _decodeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenInfoService(IDecodeRepository decodeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _decodeRepository = decodeRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TokenViewModel> GetTokenInfo()
        {
            var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            var token = authHeader?.Replace("Bearer ", string.Empty);
            var decodedInfo = _decodeRepository.Decode(token);
            if (!string.IsNullOrEmpty(decodedInfo?.UserId) && Guid.TryParse(decodedInfo.UserId, out var userId))
            {
                var tokenModel = new TokenViewModel
                {
                    UserId = new Guid(decodedInfo.UserId),
                };
                return await Task.FromResult(tokenModel);
            }
            throw new ValidateException("Invalid UserId in the token.");
        }
    }

}
