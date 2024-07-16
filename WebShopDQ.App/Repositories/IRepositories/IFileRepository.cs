using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface IFileRepository
    {
        Task<FileDTO> UploadFile(IFormFile? file);
        Task<bool> RemoveFile(string? publicId);
        Task<List<FileDTO>> UploadMulti(List<IFormFile>? formFilesDTO);
    }
}
