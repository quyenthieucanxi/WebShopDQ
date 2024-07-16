using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Data;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;

namespace WebShopDQ.App.Repositories
{
    public class FilesRepository : Repository<Files>, IFilesRepository
    {
        public FilesRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
