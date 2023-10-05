using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebShopDQ.App.Common;
using WebShopDQ.App.Common.Exceptions;
using WebShopDQ.App.Data;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.ViewModels;
using WebShopDQ.App.ViewModels.Authentication;

namespace WebShopDQ.App.Repositories
{
    public class AuthenticationRepository : Repository<User>, IAuthenticationRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly IUrlHelper _urlHelper;
        //private readonly IMapper _mapper;

        public AuthenticationRepository(DatabaseContext databaseContext, UserManager<User> userManager,
            RoleManager<Role> roleManager, IConfiguration configuration, SignInManager<User> signInManager,
            IUrlHelper urlHelper) : base(databaseContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _databaseContext = databaseContext;
            _urlHelper = urlHelper;
        }
        public async Task CheckUserByEmail(string Email)
        {
            var userByEmail = await _userManager.FindByEmailAsync(Email);
            if (userByEmail != null)
            {
                throw new DuplicateException("Email already exists!");
            }
        }
        public async Task CheckUserByUsername(string username)
        {
            var userByUsername = await _userManager.FindByNameAsync(username);
            if (userByUsername != null)
            {
                throw new DuplicateException("Username already exists!");
            }
        }
        public async Task<IdentityResult> Register(RegisterModel registerModel, string role)
        {
            //chekc user
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
                var passwordValidationResult = await passwordValidator.ValidateAsync(_userManager, null!, registerModel.Password);
                if (!passwordValidationResult.Succeeded)
                {
                    // Handle password validation errors here
                    throw new PasswordException("Password must have over 6 characters," +
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
            if (!user.IsActive)
                throw new UnauthorizedException();
            if (!login.Succeeded)
            {
                if (user != null && !user.EmailConfirmed)
                    throw new UnauthorizedException("Email has not confirmed!");
                throw new KeyNotFoundException("Wrong Email or password!");
            }
            var jwtToken = await GenerateToken(user);
            return jwtToken;
        }
        private async Task<LoginViewModel> GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? ""));
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user?.Email ?? ""),
                    new Claim("UserId", user?.Id.ToString() ?? ""),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(20),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512Signature)
            };
            var roles = await _userManager.GetRolesAsync(user!);
            foreach (var role in roles)
            {
                tokenDescription.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            // add rf token
            var refreshTokenEntity = new RefreshToken
            {
                //Id = Guid.NewGuid(),
                JwtId = token.Id,
                UserId = user!.Id ,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1)
            };
            await _databaseContext.AddAsync(refreshTokenEntity);
            await _databaseContext.SaveChangesAsync();

            return new LoginViewModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public async Task<LoginViewModel> NewToken(LoginViewModel loginViewModel)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? ""));
            var TokenValidationParameters = new TokenValidationParameters()
            {
                // provide token
                ValidateIssuer = false,
                ValidateAudience = false,

                // sign in token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? "")),

                ClockSkew = TimeSpan.Zero,

                // Not check token expired
                ValidateLifetime = false
            };
            try
            {
                //check 1: AccessToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(
                    loginViewModel.AccessToken, TokenValidationParameters, out var validatedToken);

                //check 2: Check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        throw new ValidateException("Invalidate token");
                    }
                }

                //check 3: Check accessToken expire?
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)!.Value) ;

                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    throw new ValidateException("Invalidate token");
                }

                //check 4: Check refreshtoken exist in DB
                var storedToken = _databaseContext.UserToken.FirstOrDefault(x => x.Token == loginViewModel.RefreshToken);
                if (storedToken == null)
                {
                    throw new KeyNotFoundException("Token not exist!");
                }

                //check 5: check refreshToken is used/revoked?
                if (storedToken.IsUsed)
                {
                    throw new DuplicateException("Token has been used");
                }
                if (storedToken.IsRevoked)
                {
                    throw new DuplicateException("Token has been revoked");
                }

                //check 6: AccessToken id == JwtId in RefreshToken
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)!.Value ;
                if (storedToken.JwtId != jti)
                {
                    throw new ValidateException("Token not match");
                }

                //Update token is used
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;
                _databaseContext.Update(storedToken);
                await _databaseContext.SaveChangesAsync();

                //create new token
                //var user = await _databaseContext.User.SingleOrDefaultAsync(nd => nd.Id == storedToken.UserId);
                var user = await _userManager.FindByEmailAsync(loginViewModel.AccessToken);
                var token = await GenerateToken(user);
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }
    
        public async Task<LinkedEmailModel> GetConfirmEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new KeyNotFoundException(Messages.UserNotFound);
            if (user.EmailConfirmed == true) throw new DuplicateException(Messages.ConfirmEmail);
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmLink = _urlHelper.Action("ConfirmEmail", new { token, email });
                var linkEmail = new LinkedEmailModel
                {
                    Email = email,
                    Link = confirmLink,
                };
                return await Task.FromResult(linkEmail);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<bool> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return await Task.FromResult(true);
                }
            }
            throw new KeyNotFoundException("Email not exits");
        }

        public async Task<LinkedEmailModel> ForgetPassword(ForgetPasswordModel model)
        {
            if (model.NewPassword != model.ConfirmPassword) throw new AppException("Confirm Password doesn't match Password.");
            var user = await _userManager.FindByEmailAsync(model.Email);
            var passwordValidator = new PasswordValidator<User>();
            var passwordValidate = await passwordValidator.ValidateAsync(_userManager, null!, model.NewPassword);
            List<string> passwordErrors = new List<string>();
            if (user != null)
            {
                if (passwordValidate.Succeeded)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetLink = _urlHelper.Action("ResetPassword", new { token = token, email = model.Email, pass = model.NewPassword });
                    LinkedEmailModel result = new()
                    {
                        Email = model.Email,
                        Link = resetLink
                    };
                    return await Task.FromResult(result);
                }
                else throw new PasswordException("Password must have 6 characters," +
                                "one non alphanumeric character, one digit ('0'-'9'), one uppercase, one lowercase");
            }
            else throw new KeyNotFoundException(Messages.EmailNotFound);
        }

        public async Task<IdentityResult> ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result;
        }
    }
}
