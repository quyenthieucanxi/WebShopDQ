using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
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
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        //private readonly IMapper _mapper;

        public AuthenticationRepository(DatabaseContext databaseContext, UserManager<User> userManager,
            RoleManager<Role> roleManager, IConfiguration configuration, SignInManager<User> signInManager) : base(databaseContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;

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
                UserName = registerModel.Email,
                FullName = registerModel.UserName,
            };
            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role);
                    return result;
                }
                throw new PasswordException("User failed to create!");
            }
            else throw new KeyNotFoundException("This role not exist!");
        }

        public async Task<LoginViewModel> Login(LoginModel loginModel)
        {
            var login = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false);
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (!login.Succeeded) throw new KeyNotFoundException("Wrong Email or password!");
            if (user != null && !user.EmailConfirmed) throw new UnauthorizedException("Email has not confirmed!");
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtToken = GetToken(authClaims);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            //var expiration = jwtToken.ValidTo;
            return new LoginViewModel { Token = token };
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
