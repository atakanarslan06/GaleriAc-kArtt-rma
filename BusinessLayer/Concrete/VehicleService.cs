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
        private readonly DataAccesLayer.Context.ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private ApiResponse _response;
        public VehicleService(DataAccesLayer.Context.ApplicationDbContext context, IMapper mapper, ApiResponse response)
        {
            _context = context;
            _mapper = mapper;
            _response = response;
        }

        public async Task<ApiResponse> ChangeVehicleStatus(int vehicleId)
        {
            var result = await _context.Vehicles.FindAsync(vehicleId);
            if(result == null)
            {
                _response.İsSuccess = false;
                return _response;
            }
            result.IsActive = false;
            _response.İsSuccess = true;
            await _context.SaveChangesAsync();
            return _response;
        }

        public async Task<ApiResponse> CreateVehicle(CreateVehicleDTO model)
        {
            if (model != null)
            {
                var objDTO = _mapper.Map<Vehicle>(model);
                if(objDTO != null)
                {
                    _context.Vehicles.Add(objDTO);
                    if (await _context.SaveChangesAsync()>0)
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
            var result = await _context.Vehicles.FindAsync(vehicleId);
            if (result != null)
            {
                _context.Vehicles.Remove(result);
                if (await _context.SaveChangesAsync()>0)
                {
                    _response.İsSuccess = true;
                    return _response;
                }
            }
            _response.İsSuccess=false;
            return _response;
        }

        public async Task<ApiResponse> GetVehicle()
        {
            var vehicle = await _context.Vehicles.Include(x => x.SellerId).ToListAsync();
            if (vehicle != null)
            {
                _response.İsSuccess = true;
                _response.Result=vehicle;
                return _response;
            }
            _response.İsSuccess = false;
            return _response;
        }

        public async Task<ApiResponse> GetVehicleById(int vehicleId)
        {
            var result = await _context.Vehicles.Include(x=> x.SellerId).FirstOrDefaultAsync(x=>x.VehicleId == vehicleId);
            if (result != null)
            {
                _response.Result = result;
                _response.İsSuccess = true;
                return _response;
            }
            _response.İsSuccess = false; 
            return _response;
        }

        public async Task<ApiResponse> UpdateVehicleResponse(int vehicleId, UpdateVehicleDTO model)
        {
            var result = await _context.Vehicles.FindAsync(vehicleId);
            if(result != null)
            {
                Vehicle objDTO = _mapper.Map(model, result);
                if(await _context.SaveChangesAsync()>0)
                {
                    _response.İsSuccess=true;
                    _response.Result=objDTO;
                    return _response;
                }

            }
            _response.İsSuccess = false;
            return _response;
        }
    }
}
