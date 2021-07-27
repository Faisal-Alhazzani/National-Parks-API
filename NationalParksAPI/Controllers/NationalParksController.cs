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
    [ApiController]
    //[ApiExplorerSettings(GroupName = "NationalParkRegistryV1")]
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _npRepo;
        private readonly IMapper _parksMapper;

        public NationalParksController(INationalParkRepository npRepo, IMapper parksMapper)
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

        /// <summary>
        /// Get individual national park
        /// </summary>
        /// <param name="id">The id of the national park</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(NationalParkDTO))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int id)
        {
            var foundPark = _npRepo.GetNationalPark(id);
            if (foundPark == null)
            {
                return NotFound();
            }
            var parkDTO = _parksMapper.Map<NationalParkDTO>(foundPark);
            return Ok(parkDTO);
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK , Type = typeof(NationalParkCreateDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateNationalPark([FromBody] NationalParkCreateDTO parkDTO)
        {
            if (parkDTO == null)
            {
                return BadRequest();
            }

            if (_npRepo.NationalParkExists(parkDTO.Name)){
                ModelState.AddModelError("", "National Park Already Exists!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var DbObj = _parksMapper.Map<NationalParkEntity>(parkDTO);
            if (!_npRepo.CreateNationalPark(DbObj))
            {
                ModelState.AddModelError("", $"Something went wrong! The park {parkDTO.Name} could not be added");
                return StatusCode(422, ModelState);
            }
            return Ok(_parksMapper.Map<NationalParkCreateDTO>(DbObj));
        }

        [HttpPatch("{ParkId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(NationalParkDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult UpdateNationalPark(int ParkId, NationalParkDTO parkDTO)
        {
            if (parkDTO == null || parkDTO.Id != ParkId)
            {
                return BadRequest();
            }

            if (_npRepo.NationalParkExists(parkDTO.Name))
            {
                ModelState.AddModelError("", "National Park Already Exists!");
                return StatusCode(422, ModelState);
            }

            var DbObj = _parksMapper.Map<NationalParkEntity>(parkDTO);
            if (!_npRepo.UpdateNationalPark(DbObj))
            {
                ModelState.AddModelError("", $"Something went wrong! The park {parkDTO.Name} could not be updated");
                return StatusCode(422, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{ParkId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeleteNationalPark(int ParkId)
        {
            if (!_npRepo.NationalParkExists(ParkId))
            {
                return NotFound();
            }

            var DbObj = _npRepo.GetNationalPark(ParkId);
            if (!_npRepo.DeleteNationalPark(DbObj))
            {
                ModelState.AddModelError("", $"Something went wrong! The park Id {ParkId} could not be deleted");
                return StatusCode(422, ModelState);
            }
            return NoContent();
        }
    }
}
