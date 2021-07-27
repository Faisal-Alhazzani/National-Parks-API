using NationalParksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParksAPI.Repository.IRepository
{
    public interface ITrailRepository
    {
        ICollection<TrailEntity> GetAllTrails();
        ICollection<TrailEntity> GetAllTrailsInNationalPark(int parkId);
        TrailEntity GetTrail(int nationalParkId);
        bool TrailExists(string name);
        bool TrailExists(int nationalParkId);
        bool CreateTrail(TrailEntity nationalPark);
        bool UpdateTrail(TrailEntity nationalPark);
        bool DeleteTrail(TrailEntity nationalPark);
        bool Save();
    }
}
