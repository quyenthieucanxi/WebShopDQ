using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.App.Services
{
    public class FileService : IFileService
    {
        public readonly IFileRepository _fileUploadRepository;
        public FileService(IFileRepository fileUploadRepository)
        {
            _fileUploadRepository = fileUploadRepository;
        }

        public async Task<bool> UploadFile(IFormFile? file)
        {
            var data = await _fileUploadRepository.UploadFile(file);
            return data;
        }

        public async Task<bool> RemoveFile(string? publicId)
        {
            var data = await _fileUploadRepository.RemoveFile(publicId);
            return data;
        }
    }
}
