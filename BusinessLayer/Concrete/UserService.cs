using AutoMapper;
using BusinessLayer.Abstraction;
using BusinessLayer.Dtos;
using Core.Models;
using DataAccesLayer.Context;
using DataAccesLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            DbContext dbContext, IMapper mapper, ApiResponse response, UserManager<ApplicationUser> userManager )
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
            _mapper = mapper;
            _response = response;
            _roleManager = roleManager;
            secretKey = _configuration.GetValue<string>("SecretKey: jwtKey");
        }

        public Task<ApiResponse> Login(LoginRequestDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> Register(RegisterRequestDTO model)
        {
            var userFromDb = _dbContext.applicationUsers.FirstOrDefault(
                x=>x.UserName.ToLower() == model.UserName.ToLower());
            if (userFromDb != null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.İsSuccess = false;
                _response.ErrorMessages.Add("Kullanıcı Adı Kullanılıyor");
                return _response;
            }


        }
    }
}
