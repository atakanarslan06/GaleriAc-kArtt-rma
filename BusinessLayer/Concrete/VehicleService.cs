using AutoMapper;
using BusinessLayer.Abstraction;
using BusinessLayer.Dtos;
using Core.Models;
using DataAccesLayer.Context;
using DataAccesLayer.Domain;
using Microsoft.EntityFrameworkCore;
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
        private ApiResponse _response;
        public VehicleService(DbContext dbContext, IMapper mapper, ApiResponse response)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _response = response;
        }

        public Task<ApiResponse> ChangeVehicleStatus(int vehicleId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> CreateVehicle(CreateVehicleDTO model)
        {
            if (model != null)
            {
                var objDTO = _mapper.Map<Vehicle>(model);
                if(objDTO != null)
                {
                     _dbContext.Vehicles.Add(objDTO);
                    if (await _dbContext.SaveChangesAsync()>0)
                    {
                        _response.İsSuccess = true;
                        _response.Result = model;
                        _response.StatusCode = System.Net.HttpStatusCode.Created;
                    }
                }
            }
            _response.İsSuccess = false;
            _response.ErrorMessages.Add("Ooops! Bir şeyler yanlış gitti");
            return _response;
        }

        public async Task<ApiResponse> DeleteVehicleResponse(int vehicleId)
        {
            var result = await _dbContext.Vehicles.FindAsync(vehicleId);
            if (result != null)
            {
                _dbContext.Vehicles.Remove(result);
                if (await _dbContext.SaveChangesAsync()>0)
                {
                    _response.İsSuccess = true;
                    return _response;
                }
            }
            _response.İsSuccess=false;
            return _response;
        }

        public Task<ApiResponse> GetVehicle()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> GetVehicleById(int vehicleId)
        {
            var result = await _dbContext.Vehicles.Include(x=> x.SellerId).FirstOrDefaultAsync(x=>x.VehicleId == vehicleId);
            if (result != null)
            {
                _response.Result = result;
                _response.İsSuccess = true;
                return _response;
            }
            _response.İsSuccess = false; return _response;
        }

        public Task<ApiResponse> UpdateVehicleResponse(int vehicleId, UpdateVehicleDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
