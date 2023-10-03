using AutoMapper;
using InsternShip.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Data;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<User> _repository;

        public UserRepository(DatabaseContext databaseContext, IUnitOfWork uow, IMapper mapper,
            UserManager<User> userManager, IRepository<User> repository) : base(databaseContext)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<UserInfoViewModel> GetById(Guid id)
        {
            var data = await _repository.GetById(id);
            //var data = await _userManager.FindByIdAsync(id.ToString());
            var user = _mapper.Map<UserInfoViewModel>(data);
            return user;
        }
    }
}
