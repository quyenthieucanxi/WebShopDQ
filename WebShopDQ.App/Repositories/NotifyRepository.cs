using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;

namespace WebShopDQ.App.Repositories
{
    public class NotifyRepository : Repository<Notify>, INotifyRepository
    {
        private readonly IMapper _mapper;
        public NotifyRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext)
        {
            _mapper = mapper;
        }

        public async Task Create(NotifyDTO notifyDTO)
        {
            var notify =  _mapper.Map<Notify>(notifyDTO);
            await Add(notify);
        }
    }
}
