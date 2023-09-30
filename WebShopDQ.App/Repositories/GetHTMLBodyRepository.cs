using Microsoft.AspNetCore.Hosting;
using WebShopDQ.App.Repositories.IRepositories;

namespace WebShopDQ.App.Repositories
{
    public class GetHTMLBodyRepository : IGetHTMLBodyRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public GetHTMLBodyRepository(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> GetBody(string type)
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            string bodyContentPath = Path.Combine(wwwrootPath, "Body");
            string fileName = type;
            string filePath = Path.Combine(bodyContentPath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                string content = System.IO.File.ReadAllText(filePath);
                return await Task.FromResult(content);
            }
            return await Task.FromResult(string.Empty);
        }
    }
}
