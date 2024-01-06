using BusinessLayer.Abstraction;
using BusinessLayer.Dtos;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserService : IUserService
    {
        public Task<ApiResponse> Login(LoginRequestDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> Register(RegisterRequestDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
