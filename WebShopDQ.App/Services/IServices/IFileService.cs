using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;

namespace WebShopDQ.App.Services.IServices
{
    public interface IFileService
    {
        Task<bool> UploadFile(IFormFile? file, Guid userId);
        Task<bool> RemoveFile(string? publicId);
    }
}
