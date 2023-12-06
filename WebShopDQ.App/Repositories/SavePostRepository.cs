using AutoMapper;
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
    public class SavePostRepository : Repository<SavePosts>, ISavePostRepository
    {
        private readonly IMapper _mapper;
        public SavePostRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext)
        {
            _mapper = mapper;
        }
    }
}
