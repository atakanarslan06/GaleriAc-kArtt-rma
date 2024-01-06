using DataAccesLayer.Domain;
using DataAccesLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Context
{
    public class DbContext : IdentityDbContext<ApplicationUser>
    {
        public DbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
    }
}
