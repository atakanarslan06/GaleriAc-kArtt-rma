using AutoMapper;
using BusinessLayer.Dtos;
using Core.Models;
using DataAccesLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class VehicleService : IVehicleService
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        public VehicleService(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<ApiResponse> ChangeVehicleStatus(int vehicleId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> CreateVehicle(CreateVehicleDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> DeleteVehicleResponse(int vehicleId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetVehicle()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetVehicleById(int vehicleId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateVehicleResponse(int vehicleId, UpdateVehicleDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
