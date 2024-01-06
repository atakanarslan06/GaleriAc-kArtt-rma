using AutoMapper;
using BusinessLayer.Abstraction;
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
    internal class BidService : IBidService
    {
        public BidService(ApplicationDbContext context, IMapper mapper, ApiResponse response)
        {
            _context = context;
        }

        public ApplicationDbContext _context { get; }

        public Task<ApiResponse> AutomaticallyCreateBid(CreateBidDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> CancelBid(int bidId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> CreateBid(CreateBidDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetBidById(int bidId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetBidByVehicleId(int vehicleId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateBid(int bidId, UpdateBidDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
