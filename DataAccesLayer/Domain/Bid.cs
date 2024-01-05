using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Domain
{
    public class Bid
    {
        [Key]
        public int BidId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidDate { get; set; }
        public string BidStatus { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
