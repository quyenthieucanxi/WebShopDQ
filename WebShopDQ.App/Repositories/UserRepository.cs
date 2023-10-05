using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
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
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext, IUnitOfWork uow, IMapper mapper,
            UserManager<User> userManager, IRepository<User> repository) : base(databaseContext)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
            _repository = repository;
            _databaseContext = databaseContext;
        }

        public async Task<bool> Update(Guid id, UserInfoDTO model)
        {
            var data = await _userManager.FindByIdAsync(id.ToString());
            try
            {
                _uow.BeginTransaction();
                var entry = _databaseContext.Entry(data);
                entry.CurrentValues.SetValues(model);
                _uow.SaveChanges();
                _uow.CommitTransaction();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _uow.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserListViewModel> GetAll(int page, int limit)
        {
            try
            {
                page = page != 0 ? page : 1;
                limit = limit != 0 ? limit : 10;
                var listData = new List<UserInfoViewModel>();
                var data = await Entities.OrderByDescending(user => user.CreatedTime)
                    .Where(user => user.IsActive == true && user.EmailConfirmed == true).ToListAsync();
                var totalCount = data.Count;
                data = data.Skip((page - 1) * limit).Take(limit).ToList();
                foreach (var item in data)
                {
                    var roles = await _userManager.GetRolesAsync(item);
                    var role = roles.Count > 0 ? roles[0] : "";
                    var user = _mapper.Map<UserInfoViewModel>(item);
                    user.Role = role;
                    listData.Add(user);
                }
                return new UserListViewModel
                {
                    TotalUser = totalCount,
                    UserList = listData
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
