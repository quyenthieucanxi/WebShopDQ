using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface INotifyRepository : IRepository<Notify>
    {
        Task Create(NotifyDTO notifyDTO);
    }
}
