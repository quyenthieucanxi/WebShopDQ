using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Repositories.IRepositories;

namespace WebShopDQ.App.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly Cloudinary _cloudinary;

        public FileRepository(IConfiguration configuration)
        {
            var cloudName = configuration["Cloudinary:CloudName"];
            var apiKey = configuration["Cloudinary:ApiKey"];
            var apiSecret = configuration["Cloudinary:ApiSecret"];
            Account account =  new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<FileDTO> UploadFile(IFormFile? file)
        {

            if (file != null && file!.Length > 0)
            {
                try
                {
                    var encodedFileName = WebUtility.UrlEncode(file.FileName);
                    var uploadParams = new RawUploadParams
                    {
                        //File = new FileDescription(uniqueId + "." + file.FileName.Split('.').Last(), file.OpenReadStream()),
                        File = new FileDescription(encodedFileName, file.OpenReadStream()),
                        AccessMode = "public",
                        UseFilename = true,
                        UniqueFilename = true,
                        Folder = "File"
                    };
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    var publicId = uploadResult.PublicId;
                    var publicUrl = _cloudinary.Api.UrlImgUp.Transform(new Transformation()).Version(uploadResult.Version).BuildUrl(publicId + "." + uploadResult.Format);
                     var infoFile = new FileDTO
                    {
                        Url = publicUrl,
                        PublicId = publicId
                    };
                    return infoFile;
                    //return await Task.FromResult(true);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new MissingFieldException("Choose file.");
            }
        }

        public async Task<bool> RemoveFile(string? publicId)
        {
            if (string.IsNullOrEmpty(publicId))
            {
                throw new MissingFieldException("PublicId not found!");
            }
            else
            {
                try
                {
                    DeletionParams deletionParams = new DeletionParams(publicId);
                    DeletionResult result = await _cloudinary.DestroyAsync(deletionParams);
                    return result.Result == "ok";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }

        public async Task<List<FileDTO>> UploadMulti(List<IFormFile>? files)
        {
            var filesDT0 = new List<FileDTO>();
            foreach (var file in files)
            {
                if (file != null && file!.Length > 0)
                {
                    try
                    {
                        var encodedFileName = WebUtility.UrlEncode(file.FileName);
                        var uploadParams = new RawUploadParams
                        {
                            //File = new FileDescription(uniqueId + "." + file.FileName.Split('.').Last(), file.OpenReadStream()),
                            File = new FileDescription(encodedFileName, file.OpenReadStream()),
                            AccessMode = "public",
                            UseFilename = true,
                            UniqueFilename = true,
                            Folder = "File"
                        };
                        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                        var publicId = uploadResult.PublicId;
                        var publicUrl = _cloudinary.Api.UrlImgUp.Transform(new Transformation()).Version(uploadResult.Version).BuildUrl(publicId + "." + uploadResult.Format);
                        var infoFile = new FileDTO
                        {
                            Url = publicUrl,
                            PublicId = publicId
                        };
                        filesDT0.Add(infoFile);
                        //return await Task.FromResult(true);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    throw new MissingFieldException("Choose file.");
                }
            }
            return filesDT0;
        }
    }
}
