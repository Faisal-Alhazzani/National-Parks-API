using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParksAPI.Models;
using NationalParksAPI.Models.DTOs;
using NationalParksAPI.Repository.IRepository;
using ParkyAPI.ParkyMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParksAPI.Controllers
{
    [Route("api/v{version:apiVersion}/nationalparks")]
    [ApiVersion("2.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "NationalParkRegistryV1")]
    public class NationalParksV2Controller : Controller
    {
        private readonly INationalParkRepository _npRepo;
        private readonly IMapper _parksMapper;

        public NationalParksV2Controller(INationalParkRepository npRepo, IMapper parksMapper)
        {
            _npRepo = npRepo;
            _parksMapper = parksMapper;
        }

        /// <summary>
        /// Get list of national parks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200,Type = typeof(List<NationalParkDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetNationalParks()
        {
            var objList = _npRepo.GetNationalParks();
            var DtoList = new List<NationalParkDTO>();
            foreach (var park in objList)
            {
                DtoList.Add(_parksMapper.Map<NationalParkDTO>(park));
            }

            return Ok(DtoList);
        }

    }
}
