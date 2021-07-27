using NationalParksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParksAPI.Repository.IRepository
{
    public interface INationalParkRepository
    {
        ICollection<NationalParkEntity> GetNationalParks();
        NationalParkEntity GetNationalPark(int nationalParkId);
        bool NationalParkExists(string name);
        bool NationalParkExists(int nationalParkId);
        bool CreateNationalPark(NationalParkEntity nationalPark);
        bool UpdateNationalPark(NationalParkEntity nationalPark);
        bool DeleteNationalPark(NationalParkEntity nationalPark);
        bool Save();
    }
}
