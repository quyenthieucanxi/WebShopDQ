using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebShopDQ.App.Common.Exceptions;
using WebShopDQ.App.Data;
using WebShopDQ.App.Models;
using WebShopDQ.App.Models.Authentication;
using WebShopDQ.App.Repositories.IRepositories;

namespace WebShopDQ.App.Repositories
{
    public class AuthenticationRepository : Repository<User>, IAuthenticationRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        //private readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;

        public AuthenticationRepository(DatabaseContext databaseContext, UserManager<User> userManager,
            RoleManager<Role> roleManager) : base(databaseContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Register(RegisterModel registerModel, string role)
        {
            // check user
            var userByEmail = await _userManager.FindByEmailAsync(registerModel.Email);
            if (userByEmail != null)
            {
                throw new DuplicateException("Email already exists!");
            }
            var userByUsername = await _userManager.FindByNameAsync(registerModel.UserName);
            if (userByUsername != null)
            {
                throw new DuplicateException("Username already exists!");
            }
            // Validate the password using password validators
            var passwordValidator = _userManager.PasswordValidators.FirstOrDefault();
            if (passwordValidator != null)
            {
                var passwordValidationResult = await passwordValidator.ValidateAsync(_userManager, null, registerModel.Password);
                if (!passwordValidationResult.Succeeded)
                {
                    // Handle password validation errors here
                    throw new PasswordException("Password must have 6 characters," +
                        "one non alphanumeric character, one digit ('0'-'9'), one uppercase, one lowercase");
                }
            }
            // add user
            var user = new User()
            {
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerModel.UserName,
            };
            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role);
                    return result;
                }
                throw new Common.Exceptions.InvalidOperationException("User failed to create!");
            }
            throw new KeyNotFoundException("This role not exist!");
        }
    }
}
