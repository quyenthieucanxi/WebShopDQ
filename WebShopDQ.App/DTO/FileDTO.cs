using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.DTO
{
    public class FileDTO
    {
        public string? Url { get; set; }
        public string? PublicId { get; set; }
    }

    public class FormFileDTO
    {
        public IFormFile? FormFile { get; set; }
    }
    public class FormMultiFileDTO
    {
        public List<IFormFile>? FormFile { get; set; }
    }
}
