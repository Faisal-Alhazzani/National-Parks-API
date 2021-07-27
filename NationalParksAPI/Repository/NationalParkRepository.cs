using NationalParksAPI.Data;
using NationalParksAPI.Models;
using NationalParksAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParksAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly NationalParksDbContext _db;
        public NationalParkRepository(NationalParksDbContext db)
        {
            _db = db;
        }
        public bool CreateNationalPark(NationalParkEntity nationalPark)
        {
            _db.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalParkEntity nationalPark)
        {
            _db.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalParkEntity GetNationalPark(int nationalParkId)
        {
            return _db.NationalParks.FirstOrDefault(p => p.Id == nationalParkId);
        }

        public ICollection<NationalParkEntity> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(p => p.Name).ToList();
        }

        public bool NationalParkExists(string name)
        {
            return _db.NationalParks.Any(p => p.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool NationalParkExists(int nationalParkId)
        {
            return _db.NationalParks.Any(p => p.Id == nationalParkId);
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0 ? true : false; 
        }

        public bool UpdateNationalPark(NationalParkEntity nationalPark)
        {
            _db.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
