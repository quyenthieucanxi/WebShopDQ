using AutoMapper;
using MailKit.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _dbContext;

        public UserService(IUserRepository userRepository, IMapper mapper, DatabaseContext dbContext)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<UserInfoViewModel> GetById(Guid userId)
        {
            var data = await _userRepository.GetById(userId);
            var user = _mapper.Map<UserInfoViewModel>(data);
            return user;
        }

        public async Task<UserInfoViewModel> Update(Guid userId, UserInfoDTO model)
        {
            var user = await _userRepository.GetById(userId);
            //var user = _mapper.Map<User>(model);
            //_dbContext.Entry(user).State = EntityState.Modified;
            //user.PhoneNumber = model.PhoneNumber;
            await _userRepository.Update(user);
            var updatedUser = _mapper.Map<UserInfoViewModel>(user);
            return updatedUser;
        }

        public async Task<UserListViewModel> GetAll(int page, int limit)
        {
            var data = await _userRepository.GetAll(page, limit);
            return data;
        }
    }
}
