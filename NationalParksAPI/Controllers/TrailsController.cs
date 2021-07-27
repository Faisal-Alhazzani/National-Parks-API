using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParksAPI.Models;
using NationalParksAPI.Models.DTOs;
using NationalParksAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParksAPI.Controllers
{
    [Route("api/Trails")]
    [ApiController]
    public class TrailsController : Controller
    {
        
        private readonly ITrailRepository _trailsRepo;
        private readonly IMapper _mapper;

        public TrailsController(ITrailRepository trailRepo, IMapper mapper)
        {
            _trailsRepo = trailRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of trails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200,Type = typeof(List<TrailDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetTrails()
        {
            var objList = _trailsRepo.GetAllTrails();
            var DtoList = new List<TrailDTO>();
            foreach (var park in objList)
            {
                DtoList.Add(_mapper.Map<TrailDTO>(park));
            }

            return Ok(DtoList);
        }

        /// <summary>
        /// Get individual trail by trail id
        /// </summary>
        /// <param name="id">The id of the national park</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TrailDTO))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int id)
        {
            var foundPark = _trailsRepo.GetTrail(id);
            if (foundPark == null)
            {
                return NotFound();
            }
            var trailDTO = _mapper.Map<TrailDTO>(foundPark);
            return Ok(trailDTO);
            
        }

        /// <summary>
        /// Add new trail to the system
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK , Type = typeof(TrailCreateDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateTrail([FromBody] TrailCreateDTO trailDTO)
        {
            if (trailDTO == null)
            {
                return BadRequest();
            }

            if (_trailsRepo.TrailExists(trailDTO.Name)){
                ModelState.AddModelError("", "National Park Already Exists!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var DbObj = _mapper.Map<TrailEntity>(trailDTO);
            if (!_trailsRepo.CreateTrail(DbObj))
            {
                ModelState.AddModelError("", $"Something went wrong! The park {trailDTO.Name} could not be added");
                return StatusCode(422, ModelState);
            }
            return Ok(_mapper.Map<TrailCreateDTO>(DbObj));
        }

        /// <summary>
        /// Update existing trail by trail id
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{trailId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(TrailUpdateDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult UpdateTrail(int trailId, TrailUpdateDTO trailDTO)
        {
            if (trailDTO == null || trailDTO.Id != trailId)
            {
                return BadRequest();
            }

            if (_trailsRepo.TrailExists(trailDTO.Name))
            {
                ModelState.AddModelError("", "National Park Already Exists!");
                return StatusCode(422, ModelState);
            }

            var DbObj = _mapper.Map<TrailEntity>(trailDTO);
            if (!_trailsRepo.UpdateTrail(DbObj))
            {
                ModelState.AddModelError("", $"Something went wrong! The park {trailDTO.Name} could not be updated");
                return StatusCode(422, ModelState);
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes trail by id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeleteTrail(int id)
        {
            if (!_trailsRepo.TrailExists(id))
            {
                return NotFound();
            }

            var DbObj = _trailsRepo.GetTrail(id);
            if (!_trailsRepo.DeleteTrail(DbObj))
            {
                ModelState.AddModelError("", $"Something went wrong! The park Id {id} could not be deleted");
                return StatusCode(422, ModelState);
            }
            return NoContent();
        }
    }
}
