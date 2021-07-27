using Microsoft.EntityFrameworkCore;
using NationalParksAPI.Data;
using NationalParksAPI.Models;
using NationalParksAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParksAPI.Repository.IRepository
{
    public class TrailRepository : ITrailRepository
    {

        private readonly NationalParksDbContext _db;
        public TrailRepository(NationalParksDbContext db)
        {
            _db = db;
        }
        public bool CreateTrail(TrailEntity trail)
        {
            _db.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(TrailEntity trail)
        {
            _db.Trails.Remove(trail);
            return Save();
        }

        public ICollection<TrailEntity> GetAllTrails()
        {
            return _db.Trails.Include(t => t.NationalPark).OrderBy(p => p.Name).ToList();
        }

        public ICollection<TrailEntity> GetAllTrailsInNationalPark(int parkId)
        {
            return _db.Trails.Include(t=> t.NationalPark).OrderBy(t => t.Name).Where(t => t.NationalParkId == parkId).ToList();
        }

        public TrailEntity GetTrail(int trailId)
        {
            return _db.Trails.Include(t => t.NationalPark).FirstOrDefault(p => p.Id == trailId);
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0 ? true : false;
        }

        public bool TrailExists(int trailId)
        {
            return _db.Trails.Any(p => p.Id == trailId);
        }

        public bool TrailExists(string name)
        {
            return _db.Trails.Any(p => p.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool UpdateTrail(TrailEntity trail)
        {
            _db.Trails.Update(trail);
            return Save();
        }
    }
}
