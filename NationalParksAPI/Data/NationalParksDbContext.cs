using Microsoft.EntityFrameworkCore;
using NationalParksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParksAPI.Data
{
    public class NationalParksDbContext : DbContext
    {
        public NationalParksDbContext(DbContextOptions<NationalParksDbContext> options)
            : base(options) { }
        
        public DbSet<NationalParkEntity> NationalParks { get; set; }
        public DbSet<TrailEntity> Trails { get; set; }
    }
}
