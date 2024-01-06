﻿using BusinessLayer.Dtos;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstraction
{
    public interface IBidService
    {
        Task<ApiResponse> CreateBid(CreateBidDTO model);
        Task<ApiResponse> UpdateBid(int bidId, UpdateBidDTO model);
        Task<ApiResponse> GetBidById(int bidId);
        Task<ApiResponse> CancelBid(int bidId);

        Task<ApiResponse> AutomaticallyCreateBid(CreateBidDTO model);
        Task<ApiResponse> GetBidByVehicleId(int vehicleId);
    }
}
