using BusinessLayer.Dtos;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IVehicleService
    {
        Task<ApiResponse> CreateVehicle(CreateVehicleDTO model);
        Task<ApiResponse> GetVehicle();
        Task<ApiResponse> UpdateVehicleResponse(int vehicleId, UpdateVehicleDTO model);
        Task<ApiResponse> DeleteVehicleResponse(int vehicleId);
        Task<ApiResponse> GetVehicleById(int vehicleId);
        Task<ApiResponse> ChangeVehicleStatus(int vehicleId);
    }
}
