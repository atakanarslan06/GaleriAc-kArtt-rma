using AutoMapper;
using BusinessLayer.Abstraction;
using BusinessLayer.Dtos;
using Core.Models;
using DataAccesLayer.Context;
using DataAccesLayer.Enums;
using DataAccesLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Concrete
{
    public class UserService : IUserService
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ApiResponse _response;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretKey;
        public UserService(RoleManager<IdentityRole> roleManager, IConfiguration _configuration,
            DbContext dbContext, IMapper mapper, ApiResponse response, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
            _mapper = mapper;
            _response = response;
            _roleManager = roleManager;
            secretKey = _configuration.GetValue<string>("SecretKey: jwtKey");
        }

        public async Task<ApiResponse> Login(LoginRequestDTO model)
        {
            ApplicationUser userFromDb = _dbContext.applicationUsers.FirstOrDefault(u=>u.UserName.ToLower() == model.UserName.ToLower());
            if (userFromDb != null)
            {
                bool isValid = await _userManager.CheckPasswordAsync(userFromDb, model.Password);
                if (!isValid)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Giriş Bilgileri Hatalı");
                    _response.İsSuccess = false;
                    return _response;
                }
                var role = await _userManager.GetRolesAsync(userFromDb);
                JwtSecurityTokenHandler tokenHandler = new();
                byte[] key = Encoding.ASCII.GetBytes(secretKey);

                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userFromDb.Id),
                        new Claim(ClaimTypes.Email, userFromDb.Email),
                        new Claim(ClaimTypes.Role, role.FirstOrDefault()),
                        new Claim("fullName", userFromDb.FullName),
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                };
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                LoginResponseModel _model = new()
                {
                    Email = userFromDb.Email,
                    Token = tokenHandler.WriteToken(token),
                };
                _response.Result = _model;
                _response.İsSuccess = true;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return _response;
            }
            _response.İsSuccess = false;
            _response.ErrorMessages.Add("Ooops! bir şeyler yanlış gitti");
            return _response;
        }

        public async Task<ApiResponse> Register(RegisterRequestDTO model)
        {
            var userFromDb = _dbContext.applicationUsers.FirstOrDefault(
                x => x.UserName.ToLower() == model.UserName.ToLower());
            if (userFromDb != null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.İsSuccess = false;
                _response.ErrorMessages.Add("Kullanıcı Adı Kullanılıyor");
                return _response;
            }
            var newUser = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                var isTrue = _roleManager.RoleExistsAsync(UserType.Adminstrator.ToString()).GetAwaiter().GetResult();
                if (!_roleManager.RoleExistsAsync(UserType.Adminstrator.ToString()).GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserType.Adminstrator.ToString()));
                    await _roleManager.CreateAsync(new IdentityRole(UserType.Seller.ToString()));
                    await _roleManager.CreateAsync(new IdentityRole(UserType.NormalUser.ToString()));
                }
                if (model.UserType.ToString().ToLower() == UserType.Adminstrator.ToString().ToLower())
                {
                    await _userManager.AddToRoleAsync(newUser, UserType.Adminstrator.ToString());
                }
                if (model.UserType.ToString().ToLower() == UserType.Seller.ToString().ToLower())
                {
                    await _userManager.AddToRoleAsync(newUser, UserType.Seller.ToString());
                }
                else
                {
                    await _userManager.AddToRoleAsync(newUser, UserType.NormalUser.ToString());
                }
                _response.StatusCode = System.Net.HttpStatusCode.Created;
                _response.İsSuccess = true;
                return _response;
            }
            foreach (var error in result.Errors)
            {
                _response.ErrorMessages.Add(error.ToString());
            }
            return _response;
        }
    }
}
